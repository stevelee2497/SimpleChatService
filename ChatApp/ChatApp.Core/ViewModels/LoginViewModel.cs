using ChatApp.Core.Models;
using ChatApp.Core.ViewModels.Base;
using ChatApp.Core.ViewModels.ItemTemplate;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using RestSharp;

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

		public IMvxCommand OnLoginCommand => _loginCommand ?? (_loginCommand = new MvxAsyncCommand(DoSomething));

		private string _userId;
		private HubConnection _connection;
		private IMvxCommand _loginCommand;
		private MvxObservableCollection<UserViewModel> _userViewModels;

		private async Task DoSomething()
		{
			try
			{
				_connection = new HubConnectionBuilder()
					.WithUrl("https://3a3871a6.ngrok.io/chatHub", options => { options.Headers["userId"] = UserId; })
					.Build();

				_connection.On<MessageResponse>("ReceiveMessage", message => { });

				await _connection.StartAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private async Task FetchAPI()
		{
			var client = new RestClient("http://example.com");
		}
	}
}