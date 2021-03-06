﻿using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ChatApp.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			base.Initialize();

			Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IManagementService, ManagementService>();
			Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDataModel, DataModel>();

			RegisterAppStart<LoginViewModel>();
		}
	}
}