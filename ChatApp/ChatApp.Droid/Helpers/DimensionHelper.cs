﻿using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace ChatApp.Droid.Helpers
{
	public static class DimensionHelper
	{
		public static float FromDimensionId(int dimenId)
		{
			return Android.App.Application.Context.Resources.GetDimension(dimenId);
		}

		/// <summary>
		/// Screen height in pixels
		/// </summary>
		public static int ScreenHeight => GetDefaultMetrics().HeightPixels;

		/// <summary>
		/// Screen width in pixels
		/// </summary>
		public static int ScreenWidth => GetDefaultMetrics().WidthPixels;

		public static DisplayMetrics GetDefaultMetrics()
		{
			DisplayMetrics metrics = new DisplayMetrics();
			IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
			windowManager.DefaultDisplay.GetMetrics(metrics);

			return metrics;
		}
	}
}