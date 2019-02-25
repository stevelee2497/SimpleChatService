﻿using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace ChatApp.Droid.Views.Base
{
    public abstract class BaseFragment : MvxFragment
	{
		protected abstract int LayoutId { get; }


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(LayoutId, null);
			view.SetBackgroundColor(Color.White);

			var layoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

			view.LayoutParameters = layoutParameters;
			//view.FocusChange += ViewOnFocusChange;

			InitView(view);
			CreateBinding();
			HandleActivityCommandFromFragment();
	
			return view;
		}

		protected virtual void HandleActivityCommandFromFragment()
		{		
		}

		protected virtual void InitView(View view)
		{
		}

		protected virtual void CreateBinding()
		{
		}

		public override void OnViewModelSet()
		{
			base.OnViewModelSet();

			if (ViewModel != null)
			{
				//Register Popup 
			}
		}

		//private void ViewOnFocusChange(object sender, View.FocusChangeEventArgs focusChangeEventArgs)
		//{
		//	if (focusChangeEventArgs.HasFocus)
		//	{
		//		KeyboardHelper.HideKeyboard(sender as View);
		//	}
		//}
	}
}