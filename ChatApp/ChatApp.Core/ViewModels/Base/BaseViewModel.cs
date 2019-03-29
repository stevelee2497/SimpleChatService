using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ChatApp.Core.ViewModels.Base
{
	public abstract class BaseViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		public IMvxNavigationService NavigationService => _navigationService ?? (_navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>());

		public virtual void OnActive()
		{

		}

		public virtual void OnDeActive()
		{

		}
	}
}