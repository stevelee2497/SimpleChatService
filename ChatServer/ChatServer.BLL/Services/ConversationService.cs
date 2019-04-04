using ChatServer.BLL.Extensions;
using ChatServer.BLL.Requests;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Exceptions;
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
		BaseResponse GetConversationDetail(Guid id, IDictionary<string, string> @params);
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
				.GroupBy(x => x.UserConversation.Conversation);

			var requestParams = @params.ToObject<BaseRequestParams>();

			// filter request with param @userId
			// get all conversations of user having the id = @userId
			if (requestParams.UserId != Guid.Empty)
			{
				conversations = conversations.Where(g => g.Any(c => c.UserConversation.UserId == requestParams.UserId));

				return new BaseResponse(
					HttpStatusCode.OK,
					data: conversations.Select(g => new
					{
						id = g.Key.Id,
						user = g.Key.UserConversations.Select(x => new
						{
							Id = x.UserId,
							x.User.DisplayName,
							x.User.AvatarUrl
						}).First(u => u.Id != requestParams.UserId),
						lastMessages = g.Select(x => new
						{
							x.Id,
							x.UserConversation.User.DisplayName,
							x.MessageContent,
							x.CreatedTime
						}).OrderByDescending(x => x.CreatedTime).First()
					}).OrderByDescending(x => x.lastMessages.CreatedTime));
			}

			return new BaseResponse(
				HttpStatusCode.OK,
				data: conversations.Select(g => new
				{
					id = g.Key.Id,
					users = g.GroupBy(u => u.UserConversation).Select(gg => new
					{
						userId = gg.Key.UserId,
						userConversationId = gg.Key.Id,
						gg.Key.User.DisplayName,
						gg.Key.User.AvatarUrl
					}),
					messages = g.Select(x => new
					{
						x.Id,
						x.UserConversation.User.DisplayName,
						x.MessageContent,
					})
				}));
		}

		public BaseResponse GetConversationDetail(Guid id, IDictionary<string, string> @params)
		{
			var requestParams = @params.ToObject<BaseRequestParams>();

			// filter request with param @userId
			// get all conversations of user having the id = @userId
			if (requestParams.UserId != Guid.Empty)
			{
				var c = Include(x => x.UserConversations).ThenInclude(x => x.User)
					.Include(x => x.UserConversations).ThenInclude(x => x.Messages)
					.Where(x => x.Id.Equals(id))
					.Select(x => new Conversation
					{
						Id = x.Id,
						UserConversationId = x.UserConversations.FirstOrDefault(uc => uc.UserId == requestParams.UserId).Id,
						Users = x.UserConversations.Select(uc => uc.User).Where(u => u.Id != requestParams.UserId),
						Messages = x.UserConversations.SelectMany(uc => uc.Messages).OrderByDescending(m => m.CreatedTime).Select(m => new Message
						{
							Id = m.Id,
							UserId = m.UserConversation.UserId,
							AvatarUrl = m.UserConversation.User.AvatarUrl,
							DisplayName = m.UserConversation.User.DisplayName,
							MessageContent = m.MessageContent,
							CreatedTime = m.CreatedTime
						})
					})
					.FirstOrDefault();

				
				return new BaseResponse(
					HttpStatusCode.OK,
					data: c);
			}

			throw new BadRequestException("Missing params detected");
		}

		public BaseResponse Create(ConversationRequest conversationRequest)
		{
			var isSaved = true;
			var conversation = Include(x => x.UserConversations).FirstOrDefault(x =>
				x.UserConversations.Exists(uc => uc.UserId == conversationRequest.UserId) &&
				x.UserConversations.Exists(uc => uc.UserId == conversationRequest.FriendId));

			if (conversation == null)
			{
				conversation = Create(new Conversation(), out isSaved);
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
						UserId = conversationRequest.FriendId
					}
				}, out isSaved);
			}

			if (!isSaved)
			{
				throw new InternalServerErrorException("Cannot create conversation between the 2 users");
			}

			return new BaseResponse(HttpStatusCode.OK, data: conversation.Id);
		}
	}
}