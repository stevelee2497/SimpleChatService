using System;
using ChatApp.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Core.Models.Requests;

namespace ChatApp.Core.Services
{
	public interface IManagementService
	{
		Task<List<User>> GetUserFriendList(string userId);
		Task<User> Login(int userIndex);
		Task<Conversation> FetchConversation(string conversationId);
		Task<Guid> CreateConversation(NewConversationRequest request);
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
					{"userId", userId}
				});
				return JsonConvert.DeserializeObject<List<User>>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}

		public async Task<User> Login(int userIndex)
		{
			try
			{
				var responseData = await _apiHelper.Get($"users/{userIndex}");
				return JsonConvert.DeserializeObject<User>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}

		public async Task<Conversation> FetchConversation(string conversationId)
		{
			try
			{
				var responseData = await _apiHelper.Get($"conversations/{conversationId}");
				return JsonConvert.DeserializeObject<Conversation>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}

		public async Task<Guid> CreateConversation(NewConversationRequest request)
		{
			try
			{
				var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
				var responseData = await _apiHelper.Post("conversations", content);
				return JsonConvert.DeserializeObject<Guid>(responseData.Data.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Guid.Empty;
			}
		}
	}
}