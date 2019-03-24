using System;
using System.Collections.Generic;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
	[Route("api/conversations")]
	public class ConversationController : Controller
	{
		private readonly IConversationService _conversationService;

		/// <inheritdoc />
		public ConversationController(IConversationService conversationService)
		{
			_conversationService = conversationService;
		}

		/// <summary>
		/// Get all conversations
		/// </summary>
		/// <param name="params"></param>
		/// <returns>BaseResponse</returns>
		[HttpGet]
		[Produces("application/json")]
		public BaseResponse GetAllConversation([FromHeader]IDictionary<string, string> @params)
		{
			return _conversationService.GetAllConversation(@params);
		}

		/// <summary>
		/// Get conversations of a specific user
		/// </summary>
		/// <param name="id"></param>
		/// <returns>BaseResponse</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		public BaseResponse GetConversationDetail(Guid id)
		{
			return _conversationService.GetConversationDetail(id);
		}
	}
}