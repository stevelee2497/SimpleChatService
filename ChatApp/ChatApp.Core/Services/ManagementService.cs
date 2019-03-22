using System;
using ChatApp.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
	public interface IManagementService
	{
		Task<List<User>> GetUserList(string userId);
	}

	public class ManagementService : IManagementService
	{
		private readonly RestClient _apiHelper;

		public ManagementService()
		{
			_apiHelper = new RestClient();
		}

		public async Task<List<User>> GetUserList(string userId)
		{
			try
			{
				var responseData = await _apiHelper.Get("users", parameters: new Dictionary<string, string>
				{
					{"userId", userId}
				});
				return JsonConvert.DeserializeObject<List<User>>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}