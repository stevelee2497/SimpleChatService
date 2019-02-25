using ChatApp.Core.ViewModels;
using MvvmCross.ViewModels;

namespace ChatApp.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			RegisterAppStart<HomeViewModel>();
		}
	}
}