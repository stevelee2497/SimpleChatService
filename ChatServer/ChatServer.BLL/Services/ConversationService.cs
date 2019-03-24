using ChatServer.BLL.Extensions;
using ChatServer.BLL.Requests;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ChatServer.BLL.Services
{
	public interface IConversationService : IEntityService<Conversation>
	{
		BaseResponse GetAllConversation(IDictionary<string, string> @params);
		BaseResponse GetConversationDetail(Guid id);
		BaseResponse Create(ConversationRequest conversationRequest);
	}

	public class ConversationService : EntityService<Conversation>, IConversationService
	{
		private readonly IMessageService _messageService;
		private readonly IUserConversationService _userConversationService;

		public ConversationService(IMessageService messageService, IUserConversationService userConversationService)
		{
			_messageService = messageService;
			_userConversationService = userConversationService;
		}

		public BaseResponse GetAllConversation(IDictionary<string, string> @params)
		{
			var conversations = _messageService.Include(x => x.UserConversation).ThenInclude(x => x.Conversation)
				.Include(x => x.UserConversation).ThenInclude(x => x.User)
				.GroupBy(x => x.UserConversation.ConversationId);

			var requestParams = @params.ToObject<BaseRequestParams>();

			// filter request with param @userId
			// get all conversations of user having the id = @userId
			if (requestParams.UserId != Guid.Empty)
			{
				conversations = conversations.Where(g => g.Any(c => c.UserConversation.UserId == requestParams.UserId));
			}

			return new BaseResponse(
				HttpStatusCode.OK,
				data: conversations.Select(g => new
				{
					id = g.Key,
					users = g.GroupBy(u => u.UserConversation.User).Select(gg => new
					{
						gg.Key.Id,
						gg.Key.DisplayName,
						gg.Key.AvatarUrl
					}),
					messages = g.Select(x => new
					{
						x.Id,
						x.UserConversation.User.DisplayName,
						x.MessageContent,
					})
				}));
		}

		public BaseResponse GetConversationDetail(Guid id)
		{
			return new BaseResponse(
				HttpStatusCode.OK,
				data: _messageService.Include(x => x.UserConversation).ThenInclude(x => x.Conversation)
					.Include(x => x.UserConversation).ThenInclude(x => x.User)
					.GroupBy(x => x.UserConversation.ConversationId)
					.Where(x => x.Key == id)
					.Select(g => new
				{
					id = g.Key,
					users = g.GroupBy(u => u.UserConversation.User).Select(gg => gg.Key),
					messages = g.Select(x => new
					{
						x.Id,
						x.UserConversation.User.DisplayName,
						x.MessageContent,
					})
				}));
		}

		public BaseResponse Create(ConversationRequest conversationRequest)
		{
			var isSaved = false;
			if (!IsConversationExisted(conversationRequest))
			{
				var conversation = Create(new Conversation(), out isSaved);
				_userConversationService.CreateMany(new[]
				{
					new UserConversation
					{
						ConversationId = conversation.Id,
						UserId = conversationRequest.UserId
					},
					new UserConversation
					{
						ConversationId = conversation.Id,
						UserId = conversationRequest.ReceiverId
					}
				}, out isSaved);
			}

			return isSaved ? new BaseResponse(HttpStatusCode.OK, data:"Created") : new BaseResponse(HttpStatusCode.InternalServerError);
		}

		private bool IsConversationExisted(ConversationRequest conversationRequest)
		{
			return Include(x => x.UserConversations).Any(x =>
				x.UserConversations.Exists(uc => uc.UserId == conversationRequest.UserId) &&
				x.UserConversations.Exists(uc => uc.UserId == conversationRequest.ReceiverId));
		}
	}
}