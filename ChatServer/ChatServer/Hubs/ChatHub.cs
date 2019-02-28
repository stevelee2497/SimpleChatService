﻿using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ChatServer.BLL.Services;
using ChatServer.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Hubs
{
	public class ChatHub : Hub
	{
		private readonly IUserService _userService;
		private readonly IConversationService _conversationService;
		private readonly IMessageService _messageService;

		public ChatHub(IUserService userService, IConversationService conversationService, IMessageService messageService)
		{
			_userService = userService;
			_conversationService = conversationService;
			_messageService = messageService;
		}

		public override Task OnConnectedAsync()
		{
			// find the connected user
			//var userId = Guid.Parse(Context.User.Identity.Name);
			//var user = _userService.Include(x => x.UserConversations).ThenInclude(x => x.Conversation)
			//	.Single(u => u.Id.Equals(userId));

			var user = _userService.Include(x => x.UserConversations).ThenInclude(x => x.Conversation).First();

			foreach (var conversation in user.UserConversations)
			{
				var groupName = conversation.ConversationId.ToString();
				Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			}

			return base.OnConnectedAsync();
		}
		

		public async Task SendMessage(string userId, string message)
		{
			_messageService.Create(new Message
			{
				UserConversationId = Guid.Parse(userId),
				MessageContent = message
			}, out bool isSaved);
			await Clients.Group("5C0FD2D3-84C2-4E07-9280-FCC92EF0512A").SendAsync("ReceiveMessage", userId, message);
		}

		public async Task AddToGroup(string groupId)
		{
			var conversation = _conversationService.Find(new Guid(groupId));
			await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

			await Clients.Group(groupId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupId}.");
		}

		public async Task RemoveFromGroup(string groupId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);

			await Clients.Group(groupId).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupId}.");
		}
	}
}