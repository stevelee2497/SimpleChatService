using Android.Content;
using Android.Util;
using Android.Widget;
using ChatApp.Droid.Helpers;

namespace ChatApp.Droid.Controls
{
	public class CustomButton : Button
	{
		public CustomButton(Context context) : base(context)
		{
		}

		public CustomButton(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public CustomButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		private bool _isEnable;
		public bool IsEnable
		{
			get => _isEnable;
			set
			{
				_isEnable = value;
				Activated = value;
				SetTextColor(ColorHelper.FromColorId(value ? Resource.Color.white : Resource.Color.gray));
			}
		}
	}
}