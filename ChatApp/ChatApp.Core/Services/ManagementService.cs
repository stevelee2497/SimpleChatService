using System;
using ChatApp.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
	public interface IManagementService
	{
		Task<List<User>> GetUserFriendList(string userId);
		Task<User> GetUserDetail(int userIndex);
	}

	public class ManagementService : IManagementService
	{
		private readonly RestClient _apiHelper;

		public ManagementService()
		{
			_apiHelper = new RestClient();
		}

		public async Task<List<User>> GetUserFriendList(string userId)
		{
			try
			{
				//TODO: change userIndex to userId later
				var responseData = await _apiHelper.Get("users", parameters: new Dictionary<string, string>
				{
					{"userIndex", userId}
				});
				return JsonConvert.DeserializeObject<List<User>>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}

		public async Task<User> GetUserDetail(int userIndex)
		{
			try
			{
				//TODO: change userIndex to userId later
				var responseData = await _apiHelper.Get($"users/{userIndex}");
				return JsonConvert.DeserializeObject<User>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}
}