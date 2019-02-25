using Android.App;
using Android.Content.PM;
using ChatApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ChatApp.Droid
{
	[Activity(MainLauncher = true,
		NoHistory = true,
		ScreenOrientation = ScreenOrientation.Portrait,
		Theme = "@style/SplashTheme")]
	public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
	{
		public SplashScreen() : base(Resource.Layout.splash_screen)
		{
		}
	}
}