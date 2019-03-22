using ChatServer.BLL.Responses;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ChatServer.BLL.Services
{
	public interface IConversationService : IEntityService<Conversation>
	{
		BaseResponse GetAllConversation(IDictionary<string, string> @params);
	}

	public class ConversationService : EntityService<Conversation>, IConversationService
	{
		private IUserConversationService _userConversationService;

		public ConversationService(IUserConversationService userConversationService)
		{
			_userConversationService = userConversationService;
		}

		public BaseResponse GetAllConversation(IDictionary<string, string> @params)
		{
			var conversations = All();

			return new BaseResponse(HttpStatusCode.OK, data: conversations.Include(x => x.UserConversations)
				.ThenInclude(x => x.User)
				.Select(x => new
				{
					x.Id,
					Users = x.UserConversations.Select(us => us.User),
					Messages = x.UserConversations.Select(uc => uc.Messages.Select(m => new
					{
						m.Id,
						user = uc.User.DisplayName,
						m.MessageContent
					}))
				}));
		}
	}
}