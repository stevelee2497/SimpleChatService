using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using ChatApp.Droid.Helpers;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace ChatApp.Droid.Controls
{
	public class CustomRecyclerView : MvxRecyclerView
	{
		public CustomRecyclerView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public CustomRecyclerView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public CustomRecyclerView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
		}

		public CustomRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter) : base(context, attrs, defStyle, adapter)
		{
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			heightMeasureSpec = MeasureSpec.MakeMeasureSpec(DimensionHelper.ScreenHeight / 3, MeasureSpecMode.AtMost);
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
		}
	}
}