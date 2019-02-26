using System;
using Android.Util;
using Android.Widget;

namespace ChatApp.Droid.Controls
{
	public class ScrollViewWithMaxHeight : ScrollView
	{
		private const int WithoutMaxHeight = -1;
		private int maxHeight = WithoutMaxHeight;

		public ScrollViewWithMaxHeight(Android.Content.Context context) : base(context)
		{
		}

		public ScrollViewWithMaxHeight(Android.Content.Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public ScrollViewWithMaxHeight(Android.Content.Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public ScrollViewWithMaxHeight(Android.Content.Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			try
			{
				int heightSize = MeasureSpec.GetSize(heightMeasureSpec);
				if (maxHeight != WithoutMaxHeight
						&& heightSize > maxHeight)
				{
					heightSize = maxHeight;
				}
				heightMeasureSpec = MeasureSpec.MakeMeasureSpec(heightSize, Android.Views.MeasureSpecMode.AtMost);
				this.LayoutParameters.Height = heightSize;
			}
			catch (Exception e)
			{
			}
			finally
			{
				base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			}
		}

		public void SetMaxHeight(int maxHeight)
		{
			this.maxHeight = maxHeight;
		}
	}
}