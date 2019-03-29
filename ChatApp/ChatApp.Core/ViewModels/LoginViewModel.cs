using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Core.ViewModels.Base;
using ChatApp.Core.ViewModels.ItemTemplate;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public MvxObservableCollection<UserViewModel> UserViewModels
		{
			get => _userViewModels;
			set => SetProperty(ref _userViewModels, value);
		}

		public string UserIndex
		{
			get => _userIndex;
			set => SetProperty(ref _userIndex, value);
		}

		public string UserName
		{
			get => _userName;
			set => SetProperty(ref _userName, value);
		}

		public IMvxCommand OnLoginCommand => _loginCommand ?? (_loginCommand = new MvxAsyncCommand(Login));

		private string _userIndex;
		private string _userName;
		private HubConnection _connection;
		private IMvxCommand _loginCommand;
		private readonly IDataModel _dataModel;
		private readonly IManagementService _managementService;
		private MvxObservableCollection<UserViewModel> _userViewModels;

		public LoginViewModel(IManagementService managementService, IDataModel dataModel)
		{
			_dataModel = dataModel;
			_managementService = managementService;
			UserViewModels = new MvxObservableCollection<UserViewModel>();
		}

		private async Task Login()
		{
			try
			{
				_dataModel.User = await _managementService.Login(Convert.ToInt32(UserIndex));
				UserName = _dataModel.User.DisplayName;

				_connection = new HubConnectionBuilder()
					.WithUrl(AppConstants.ChatHubUrl, options => { options.Headers["userId"] = _dataModel.User.Id; })
					.Build();

				_connection.On<Message>("ReceiveMessage", message => { });

				UserViewModels.Clear();
				UserViewModels.AddRange((await _managementService.GetUserFriendList(_dataModel.User.Id))
					.Select(x => new UserViewModel(x)));

				await _connection.StartAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}