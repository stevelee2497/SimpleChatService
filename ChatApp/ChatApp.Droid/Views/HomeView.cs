using Android.App;
using Android.Content.PM;
using ChatApp.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ChatApp.Droid.Views
{
	[MvxActivityPresentation]
	[Activity(Label = "Home View", ScreenOrientation = ScreenOrientation.Portrait)]
	public class HomeView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.home_view;

		protected override void InitView()
		{
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
		}
	}
}