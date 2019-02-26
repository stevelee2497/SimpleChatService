using System.Collections.Generic;
using System.Linq;
using ChatApp.Core.Models;
using ChatApp.Core.ViewModels.Base;
using ChatApp.Core.ViewModels.ItemTemplate;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace ChatApp.Core.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		#region Properties

		public string SendMessageHint => "Gõ tin nhắn ...";

		public MvxObservableCollection<MessageItemViewModel> MessageItemViewModels
		{
			get => _messageItemViewModels;
			set => SetProperty(ref _messageItemViewModels, value);
		}

		public IMvxCommand SendCommentCommand => _sendCommentCommand ?? new MvxCommand(DoSomething);

		public bool IsSendIconActivated { get; set; } = true;

		private MvxObservableCollection<MessageItemViewModel> _messageItemViewModels;
		private readonly List<Message> _messages;
		private readonly List<User> _users;
		private IMvxCommand _sendCommentCommand;
		HubConnection hubConnection;

		#endregion

		public HomeViewModel()
		{
			_users = new List<User> {new User {DisplayName = "Quoc Tran"}, new User {DisplayName = "Other User"}};
			_messages = new List<Message>
			{
				new Message {MessageContent = "Ai mang theo gió đêm qua chút hương thơm cỏ dại", User = _users[0]},
				new Message {MessageContent = "Ai lay cho đám mân côi đong đưa ngoài hiên", User = _users[0]},
				new Message {MessageContent = "Ai thêm con nắng trong veo nhắc sen hồng nở muộn. Ai mang hoa đên cho em..em chờ", User = _users[0]},
				new Message {MessageContent = "Em còn đợi ai trong ngày xanh non biếc thế?", User = _users[1]},
				new Message {MessageContent = "Thôi đừng chờ chi cho phí hết xuân xanh ngời", User = _users[1]},
				new Message {MessageContent = "Ai mang theo gió đêm qua chút hương thơm cỏ dại", User = _users[0]},
				new Message {MessageContent = "Ai lay cho đám mân côi đong đưa ngoài hiên", User = _users[0]},
				new Message {MessageContent = "Ai thêm con nắng trong veo nhắc sen hồng nở muộn. Ai mang hoa đên cho em..em chờ", User = _users[0]},
				new Message {MessageContent = "Em còn đợi ai trong ngày xanh non biếc thế?", User = _users[1]},
				new Message {MessageContent = "Thôi đừng chờ chi cho phí hết xuân xanh ngời", User = _users[1]},
				new Message {MessageContent = "Ai mang theo gió đêm qua chút hương thơm cỏ dại", User = _users[0]},
				new Message {MessageContent = "Ai lay cho đám mân côi đong đưa ngoài hiên", User = _users[0]},
				new Message {MessageContent = "Ai thêm con nắng trong veo nhắc sen hồng nở muộn. Ai mang hoa đên cho em..em chờ", User = _users[0]},
				new Message {MessageContent = "Em còn đợi ai trong ngày xanh non biếc thế?", User = _users[1]},
				new Message {MessageContent = "Thôi đừng chờ chi cho phí hết xuân xanh ngời", User = _users[1]},
				new Message {MessageContent = "Ai mang theo gió đêm qua chút hương thơm cỏ dại", User = _users[0]},
				new Message {MessageContent = "Ai lay cho đám mân côi đong đưa ngoài hiên", User = _users[0]},
				new Message {MessageContent = "Ai thêm con nắng trong veo nhắc sen hồng nở muộn. Ai mang hoa đên cho em..em chờ", User = _users[0]},
				new Message {MessageContent = "Em còn đợi ai trong ngày xanh non biếc thế?", User = _users[1]},
				new Message {MessageContent = "Thôi đừng chờ chi cho phí hết xuân xanh ngời", User = _users[1]},
				new Message {MessageContent = "Ai mang theo gió đêm qua chút hương thơm cỏ dại", User = _users[0]},
				new Message {MessageContent = "Ai lay cho đám mân côi đong đưa ngoài hiên", User = _users[0]},
				new Message {MessageContent = "Ai thêm con nắng trong veo nhắc sen hồng nở muộn. Ai mang hoa đên cho em..em chờ", User = _users[0]},
				new Message {MessageContent = "Em còn đợi ai trong ngày xanh non biếc thế?", User = _users[1]},
				new Message {MessageContent = "Thôi đừng chờ chi cho phí hết xuân xanh ngời", User = _users[1]},
				new Message {MessageContent = "Ai mang theo gió đêm qua chút hương thơm cỏ dại", User = _users[0]},
				new Message {MessageContent = "Ai lay cho đám mân côi đong đưa ngoài hiên", User = _users[0]},
				new Message {MessageContent = "Ai thêm con nắng trong veo nhắc sen hồng nở muộn. Ai mang hoa đên cho em..em chờ", User = _users[0]},
				new Message {MessageContent = "Em còn đợi ai trong ngày xanh non biếc thế?", User = _users[1]},
				new Message {MessageContent = "Thôi đừng chờ chi cho phí hết xuân xanh ngời", User = _users[1]},
			};
			MessageItemViewModels = new MvxObservableCollection<MessageItemViewModel>(_messages.Select(ConvertToItemViewModel));

			// localhost for UWP/iOS or special IP for Android
			var ip = "10.0.2.2";

			hubConnection = new HubConnectionBuilder()
				.WithUrl($"http://{ip}:5000/chatHub")
				.Build();
		}

		private MessageItemViewModel ConvertToItemViewModel(Message message) =>
			new MessageItemViewModel(message.MessageContent, null, _users[0].Equals(message.User));

		private void DoSomething()
		{
			
		}
	}
}