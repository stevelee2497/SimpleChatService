using ChatServer.BLL.Services;
using ChatServer.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatServer.DB
{
	internal static class DbInitializer
	{
		public static void Seed(IServiceProvider services)
		{
			SeedUser(services);
			SeedConversation(services);
			SeedMessages(services);
		}

		private static void SeedUser(IServiceProvider services)
		{
			var userService = services.GetService<IUserService>();
			if (userService.Count() > 0)
			{
				return;
			}

			var users = new List<User>
			{
				new User {DisplayName = "Khang Lê"},
				new User {DisplayName = "Tài Quốc"},
				new User {DisplayName = "Lâm Nguyễn"},
				new User {DisplayName = "Quốc Trần"}
			};
			userService.CreateMany(users, out var isSaved);
		}

		private static void SeedConversation(IServiceProvider services)
		{
			var conversationService = services.GetService<IConversationService>();
			var userService = services.GetService<IUserService>();
			var userConversationService = services.GetService<IUserConversationService>();

			if (conversationService.Count() > 0)
			{
				return;
			}

			var userInConversation = userService.All().Take(2);
			var conversation = conversationService.Create(new Conversation(), out var isSaved);
			if (isSaved)
			{
				userConversationService.CreateMany(
					new[]
					{
						new UserConversation() {UserId = userInConversation.First().Id, ConversationId = conversation.Id},
						new UserConversation() {UserId = userInConversation.Last().Id, ConversationId = conversation.Id},
					},
					out isSaved);
			}
		}

		private static void SeedMessages(IServiceProvider services)
		{
			var messageService = services.GetService<IMessageService>();
			var userConversationService = services.GetService<IUserConversationService>();

			if (messageService.Count() > 0)
			{
				return;
			}

			var userA = userConversationService.All().First();
			var userB = userConversationService.All().Last();

			messageService.CreateMany(new[]
			{
				new Message { UserConversationId = userA.Id, MessageContent = "A test"},
				new Message { UserConversationId = userB.Id, MessageContent = "B test"},
				new Message { UserConversationId = userA.Id, MessageContent = "A test"},
				new Message { UserConversationId = userA.Id, MessageContent = "A test"},
				new Message { UserConversationId = userB.Id, MessageContent = "B test"},
			}, out _);
		}
	}
}