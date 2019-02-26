using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using FFImageLoading;
using FFImageLoading.Cross;

namespace ChatApp.Droid.Controls
{
	public class CustomMvxCachedImageView : MvxCachedImageView
    {
        public string ImageUrl
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
	                value = LoadingPlaceholderImagePath;
                }
	            ImagePath = value;
			}
        }

	    public string Base64String
	    {
		    set
		    {
			    if (string.IsNullOrEmpty(value))
			    {
				    return;
			    }
			    ImageService.Instance.LoadBase64String($"data:image/png;base64,{value}").Transform(Transformations).Into(this);
		    }
	    }
		
		public CustomMvxCachedImageView(Context context) : base(context)
	    {
		    
		}

	    public CustomMvxCachedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
	    {
	    }

	    public CustomMvxCachedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
	    {
	    }
    }
}