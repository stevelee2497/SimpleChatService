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
				var conversation = _messageService.Include(x => x.UserConversation).ThenInclude(x => x.Conversation)
					.Include(x => x.UserConversation).ThenInclude(x => x.User)
					.OrderByDescending(x => x.CreatedTime)
					.GroupBy(x => x.UserConversation.Conversation)
					.Where(x => x.Key.Id == id)
					.Select(g => new
					{
						id = g.Key.Id,
						userConversationId = g.Key.UserConversations.First(x => x.UserId == requestParams.UserId).Id,
						user = g.Key.UserConversations.Select(x => new
						{
							Id = x.UserId,
							x.User.DisplayName,
							x.User.AvatarUrl
						}).First(u => u.Id != requestParams.UserId),
						messages = g.Select(x => new
							{
								x.Id,
								x.UserConversation.UserId,
								x.UserConversation.User.AvatarUrl,
								x.UserConversation.User.DisplayName,
								x.MessageContent,
								x.CreatedTime
							})
					})
					.First();
				return new BaseResponse(
					HttpStatusCode.OK,
					data: conversation);
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