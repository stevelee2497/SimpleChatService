using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.Core.Models;
using ChatApp.Core.ViewModels.Base;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using MvvmCross.Commands;

namespace ChatApp.Core.ViewModels.ItemTemplate
{
	public class UserViewModel : BaseViewModel
	{
		public List<ITransformation> Transformations => new List<ITransformation> { new CircleTransformation() };

		public string AvatarUrl
		{
			get => _avatarUrl;
			set => SetProperty(ref _avatarUrl, value);
		}

		public string UserDisplayName
		{
			get => _userDisplayName;
			set => SetProperty(ref _userDisplayName, value);
		}

		public IMvxCommand OnItemClick => _onItemClick ?? (_onItemClick = new MvxAsyncCommand(OpenConversation));

		private User _user;
		private string _avatarUrl;
		private string _userDisplayName;
		private IMvxCommand _onItemClick;
		

		public UserViewModel(User user)
		{
			_user = user;
			_avatarUrl = user.AvatarUrl;
			_userDisplayName = user.DisplayName;
		}

		private Task OpenConversation()
		{
			return NavigationService.Navigate<ConversationViewModel, User>(_user);
		}
	}
}