using ChatApp.Core.Models;
using ChatApp.Core.ViewModels.Base;
using ChatApp.Core.ViewModels.ItemTemplate;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Core.Services;

namespace ChatApp.Core.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public MvxObservableCollection<UserViewModel> UserViewModels
		{
			get => _userViewModels;
			set => SetProperty(ref _userViewModels, value);
		}

		public string UserId
		{
			get => _userId;
			set => SetProperty(ref _userId, value);
		}

		public IMvxCommand OnLoginCommand => _loginCommand ?? (_loginCommand = new MvxAsyncCommand(GetUsers));

		private string _userId;
		private HubConnection _connection;
		private IMvxCommand _loginCommand;
		private readonly IManagementService _managementService;
		private MvxObservableCollection<UserViewModel> _userViewModels;

		public LoginViewModel(IManagementService managementService)
		{
			_managementService = managementService;
			UserViewModels = new MvxObservableCollection<UserViewModel>();
		}

		private async Task GetUsers()
		{
			try
			{
				_connection = new HubConnectionBuilder()
					.WithUrl("https://3a3871a6.ngrok.io/chatHub", options => { options.Headers["userId"] = UserId; })
					.Build();

				_connection.On<MessageResponse>("ReceiveMessage", message => { });

				UserViewModels.AddRange((await _managementService.GetUserList(UserId))
					.Select(x => new UserViewModel(x.AvatarUrl, x.DisplayName)));

				await _connection.StartAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}