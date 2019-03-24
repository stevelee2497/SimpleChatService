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
		private readonly IMessageService _messageService;

		public ConversationService(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public BaseResponse GetAllConversation(IDictionary<string, string> @params) => new BaseResponse(
			HttpStatusCode.OK,
			data: _messageService.Include(x => x.UserConversation).ThenInclude(x => x.Conversation)
				.Include(x => x.UserConversation).ThenInclude(x => x.User)
				.GroupBy(x => x.UserConversation.ConversationId)
				.Select(g => new
				{
					g.Key,
					users = g.GroupBy(u => u.UserConversation.User).Select(gg => gg.Key),
					messages = g.Select(x => new
					{
						x.Id,
						x.UserConversation.User.DisplayName,
						x.MessageContent,
					})
				}));
	}
}