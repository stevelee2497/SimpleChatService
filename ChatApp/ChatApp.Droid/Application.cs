using Android.App;
using Android.Runtime;
using ChatApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
using System;

namespace ChatApp.Droid
{
	[Application]
	public class Application : MvxAppCompatApplication<Setup, App>
	{
		public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}
	}
}