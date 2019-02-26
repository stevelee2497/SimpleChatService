using System;
using Android.Content;
using Android.Util;

namespace ChatApp.Droid.Helpers
{
	public static class ConvertHelper
	{
		public static int DpToPx(string dp, Context context)
		{
			return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)Convert.ToDouble(dp), context.Resources.DisplayMetrics);
		}
		public static int SpToPx(string sp, Context context)
		{
			return (int)TypedValue.ApplyDimension(ComplexUnitType.Sp, (float)Convert.ToDouble(sp), context.Resources.DisplayMetrics);
		}
		public static int DpToSp(string dp, Context context)
		{
			return (int)(DpToPx(dp, context) / context.Resources.DisplayMetrics.ScaledDensity);
		}
	}
}