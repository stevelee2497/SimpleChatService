using ChatServer.BLL.Requests;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services;
using ChatServer.DAL.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
			var userId = Context.GetHttpContext().Request.Headers["userId"];

			var user = _userService.Include(x => x.UserConversations)
				.ThenInclude(x => x.Conversation)
				.ToList()
				.ElementAt(Convert.ToInt32(userId));

			foreach (var conversation in user.UserConversations)
			{
				var groupName = conversation.ConversationId.ToString().ToLower();
				Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			}

			return base.OnConnectedAsync();
		}

		// ReSharper disable UnusedMember.Global
		public async Task SendMessage(MessageRequest messageRequest)
		{
			_messageService.Create(new Message
			{
				UserConversationId = Guid.Parse(messageRequest.UserConversationId),
				MessageContent = messageRequest.MessageContent
			}, out _);
			await Clients.Group(messageRequest.ConversationId.ToLower()).SendAsync("ReceiveMessage", new MessageResponse
			{
				MessageContent = messageRequest.MessageContent,
				UserDisplayName = _userService.Find(Guid.Parse(messageRequest.UserId)).DisplayName
			});
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