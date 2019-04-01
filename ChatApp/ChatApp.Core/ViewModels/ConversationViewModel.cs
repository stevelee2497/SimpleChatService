using ChatApp.Core.Models;
using ChatApp.Core.Models.Requests;
using ChatApp.Core.Services;
using ChatApp.Core.ViewModels.Base;
using ChatApp.Core.ViewModels.ItemTemplate;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Core.ViewModels
{
	public class ConversationViewModel : BaseViewModel, IMvxViewModel<User>
	{
		#region Properties

		public string SendMessageHint => "Gõ tin nhắn ...";

		public List<ITransformation> Transformations => new List<ITransformation> {new CircleTransformation()};

		public MvxObservableCollection<MessageItemViewModel> MessageItemViewModels
		{
			get => _messageItemViewModels;
			set => SetProperty(ref _messageItemViewModels, value);
		}

		public string FriendAvatarUrl
		{
			get => _friendAvatarUrl;
			set => SetProperty(ref _friendAvatarUrl, value);
		}

		public string FriendUserName
		{
			get => _friendUserName;
			set => SetProperty(ref _friendUserName, value);
		}

		public string Message
		{
			get => _message;
			set => SetProperty(ref _message, value);
		}

		public IMvxCommand SendCommentCommand =>
			_sendCommentCommand ?? (_sendCommentCommand = new MvxAsyncCommand(SendMessage));

		public IMvxInteraction ScrollToBottom =>
			_scrollToBottom ?? (_scrollToBottom = new MvxInteraction());

		public bool IsSendIconActivated { get; set; } = true;

		private User _friend;
		private string _message;
		private string _friendUserName;
		private string _friendAvatarUrl;
		private string _ourUserConversationId;
		private HubConnection _connection;
		private Conversation _conversation;
		private IMvxCommand _sendCommentCommand;
		private MvxInteraction _scrollToBottom;
		private readonly IDataModel _dataModel;
		private readonly IManagementService _managementService;
		private MvxObservableCollection<MessageItemViewModel> _messageItemViewModels;

		#endregion

		public ConversationViewModel(IManagementService managementService, IDataModel dataModel)
		{
			_dataModel = dataModel;
			_managementService = managementService;
			MessageItemViewModels = new MvxObservableCollection<MessageItemViewModel>();
		}

		public void Prepare(User friend)
		{
			_friend = friend;
			FriendAvatarUrl = friend.AvatarUrl;
			FriendUserName = friend.DisplayName;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			var conversationId = await _managementService.CreateConversation(new NewConversationRequest
			{
				UserId = _dataModel.User.Id,
				FriendId = _friend.Id
			});

			_conversation = await _managementService.FetchConversation(conversationId);
			_ourUserConversationId = _conversation.Users.First(u => !u.Id.ToUpper().Equals(_friend.Id.ToUpper())).UserConversationId;

			MessageItemViewModels =
				new MvxObservableCollection<MessageItemViewModel>(_conversation.Messages.Select(ConvertToItemViewModel));

			_connection = new HubConnectionBuilder()
				.WithUrl(AppConstants.ChatHubUrl, options => { options.Headers["userId"] = _dataModel.User.Id; })
				.Build();

			_connection.On<Message>("ReceiveMessage", ReceiveMessage);

			await _connection.StartAsync();
		}

		private MessageItemViewModel ConvertToItemViewModel(Message message) =>
			new MessageItemViewModel(message, _friend.Id.Equals(message.UserId));

		private async Task SendMessage()
		{
			var message = new Message
			{
				UserId = _dataModel.User.Id,
				UserConversationId = _ourUserConversationId,
				ConversationId = _conversation.Id,
				MessageContent = Message,
			};
			await _connection.SendAsync("SendMessage", message);
			Message = "";
		}

		private void ReceiveMessage(Message message)
		{
			message.AvatarUrl = FriendAvatarUrl;
			MessageItemViewModels.Insert(0, new MessageItemViewModel(message, message.UserId.ToUpper().Equals(_friend.Id.ToUpper())));
			_scrollToBottom.Raise();
		}
	}
}