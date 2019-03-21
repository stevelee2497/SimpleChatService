using System.Collections.Generic;
using ChatApp.Core.ViewModels.Base;
using FFImageLoading.Transformations;
using FFImageLoading.Work;

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

		private string _avatarUrl;
		private string _userDisplayName;

		public UserViewModel(string avatarUrl, string userDisplayName)
		{
			_avatarUrl = avatarUrl;
			_userDisplayName = userDisplayName;
		}
	}
}