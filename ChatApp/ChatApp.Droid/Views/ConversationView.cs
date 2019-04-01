using Android.App;
using Android.Content.PM;
using Android.Support.V7.Widget;
using ChatApp.Droid.Views.Base;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;
using ChatApp.Core.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace ChatApp.Droid.Views
{
	[MvxActivityPresentation]
	[Activity(Label = "Home View", ScreenOrientation = ScreenOrientation.Portrait)]
	public class ConversationView : BaseActivity
	{
		public IMvxInteraction ScrollToBottomInteraction
		{
			get => _scrollToBottomInteraction;
			set
			{
				if (_scrollToBottomInteraction != null)
					_scrollToBottomInteraction.Requested -= OnInteractionRequested;

				_scrollToBottomInteraction = value;
				_scrollToBottomInteraction.Requested += OnInteractionRequested;
			}
		}

		protected override int LayoutId => Resource.Layout.conversation_view;

		private MvxRecyclerView _rvMessages;
		private IMvxInteraction _scrollToBottomInteraction;
		private LinearLayoutManager _layoutManager;

		protected override void InitView()
		{
			_rvMessages = FindViewById<MvxRecyclerView>(Resource.Id.rvMessages);
			_layoutManager = new LinearLayoutManager(this) {ReverseLayout = true};
			_rvMessages.SetLayoutManager(_layoutManager);
		}

		protected override void CreateBinding()
		{
			base.CreateBinding();
			var set = this.CreateBindingSet<ConversationView, ConversationViewModel>();

			set.Bind(this)
				.For(v => v.ScrollToBottomInteraction)
				.To(vm => vm.ScrollToBottom)
				.OneWay();

			set.Apply();
		}

		private void OnInteractionRequested(object sender, EventArgs e)
		{
			_layoutManager.SmoothScrollToPosition(_rvMessages, new RecyclerView.State(), 0);
		}
	}
}