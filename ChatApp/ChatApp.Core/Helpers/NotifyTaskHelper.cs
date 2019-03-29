using System;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace ChatApp.Core.Helpers
{
	public static class NotifyTaskHelper
	{
		public static MvxNotifyTask Create(Func<Task> task)
		{
			return MvxNotifyTask.Create(
				async () =>
				{
					try
					{
						await task.Invoke();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				});
		}
	}
}