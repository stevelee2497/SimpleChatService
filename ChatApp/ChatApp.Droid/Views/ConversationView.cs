using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V7.Widget;
using ChatApp.Droid.Views.Base;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ChatApp.Droid.Views
{
	[MvxActivityPresentation]
	[Activity(Label = "Home View", ScreenOrientation = ScreenOrientation.Portrait)]
	public class ConversationView : BaseActivity
	{
		protected override int LayoutId => Resource.Layout.conversation_view;

		protected override void InitView()
		{
			FindViewById<MvxRecyclerView>(Resource.Id.rvMessages).SetLayoutManager(new LinearLayoutManager(this)
				{ReverseLayout = true});
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
		}
	}
}