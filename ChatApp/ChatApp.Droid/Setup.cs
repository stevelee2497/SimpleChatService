using ChatApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System.Collections.Generic;
using System.Reflection;

namespace ChatApp.Droid
{
	public class Setup : MvxAppCompatSetup<App>
	{
		protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
		{
			typeof(MvxRecyclerView).Assembly
		};
	}
}