using ChatApp.Core.ViewModels.Base;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using System.Collections.Generic;

namespace ChatApp.Core.ViewModels.ItemTemplate
{
	public class MessageItemViewModel : BaseViewModel
	{
		public List<ITransformation> Transformations => new List<ITransformation> { new CircleTransformation() };

		public string MessageContent
		{
			get => _messageContent;
			set => SetProperty(ref _messageContent, value);
		}

		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public bool IsOthersMessages
		{
			get => _isOthersMessages;
			set => SetProperty(ref _isOthersMessages, value);
		}

		private string _messageContent;
		private string _avatarUrl;
		private bool _isOthersMessages;

		public MessageItemViewModel(string messageContent, string avatarUrl, bool isOthersMessages)
		{
			_messageContent = messageContent;
			_avatarUrl = avatarUrl;
			_isOthersMessages = isOthersMessages;
		}
	}
}