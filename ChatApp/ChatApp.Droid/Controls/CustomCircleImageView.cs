﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using FFImageLoading.Cross;
using FFImageLoading.Transformations;
using FFImageLoading.Work;

namespace ChatApp.Droid.Controls
{
	public class CustomCircleImageView : MvxCachedImageView
    {
        public string ImageUrl
        {
            set
            {
				if (!string.IsNullOrEmpty(value))
                {
	                Transformations = new List<ITransformation> { new CircleTransformation() };
					ImagePath = value;
				}
                else
				{
					SetImageDrawable(Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1
						? Resources.GetDrawable(Resource.Drawable.default_avatar, null)
						: ContextCompat.GetDrawable(Android.App.Application.Context, Resource.Drawable.default_avatar));
				}
            }
        }

	    public CustomCircleImageView(Context context) : base(context)
	    {
	    }

	    public CustomCircleImageView(Context context, IAttributeSet attrs) : base(context, attrs)
	    {
	    }

	    public CustomCircleImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
	    {
	    }
    }
}