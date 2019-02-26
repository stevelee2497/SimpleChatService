using ChatApp.Core.ViewModels.ItemTemplate;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace ChatApp.Droid.TemplateSelectors
{
	public class MessageTemplateSelector : MvxTemplateSelector<MessageItemViewModel>
	{
		public override int GetItemLayoutId(int fromViewType)
		{
			return fromViewType;
		}

		protected override int SelectItemViewType(MessageItemViewModel forItemObject)
		{
			return forItemObject.IsOthersMessages ? Resource.Layout.message_template_others : Resource.Layout.message_template_ours;
		}
	}
}