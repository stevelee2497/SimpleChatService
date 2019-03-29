using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Core.ViewModels.Base;
using ChatApp.Core.ViewModels.ItemTemplate;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace ChatApp.Core.ViewModels
{
	public class ConversationViewModel : BaseViewModel, IMvxViewModel<User>
	{
		#region Properties

		public string SendMessageHint => "Gõ tin nhắn ...";

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

		public IMvxCommand SendCommentCommand => _sendCommentCommand ?? (_sendCommentCommand = new MvxCommand(DoSomething));

		public bool IsSendIconActivated { get; set; } = true;

		private User _friend;
		private IMvxCommand _sendCommentCommand;
		private MvxObservableCollection<MessageItemViewModel> _messageItemViewModels;
		private string _friendAvatarUrl;
		private string _friendUserName;
		private Conversation _conversation;
		private IManagementService _managementService;

		#endregion

		public ConversationViewModel(IManagementService managementService)
		{
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

			_conversation = await _managementService.FetchConversation(null);
		}

		private MessageItemViewModel ConvertToItemViewModel(Message message) =>
			new MessageItemViewModel(message.MessageContent, null, _friend.Equals(message.User));

		private void DoSomething()
		{
			
		}
	}
}