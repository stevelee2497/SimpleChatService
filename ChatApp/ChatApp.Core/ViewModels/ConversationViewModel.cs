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

		public IMvxCommand SendCommentCommand =>
			_sendCommentCommand ?? (_sendCommentCommand = new MvxCommand(DoSomething));

		public bool IsSendIconActivated { get; set; } = true;

		private User _friend;
		private string _friendUserName;
		private string _friendAvatarUrl;
		private Conversation _conversation;
		private IMvxCommand _sendCommentCommand;
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

			MessageItemViewModels =
				new MvxObservableCollection<MessageItemViewModel>(_conversation.Messages.Select(ConvertToItemViewModel));
		}

		private MessageItemViewModel ConvertToItemViewModel(Message message) =>
			new MessageItemViewModel(message, _friend.Id.Equals(message.UserId));

		private void DoSomething()
		{
		}
	}
}