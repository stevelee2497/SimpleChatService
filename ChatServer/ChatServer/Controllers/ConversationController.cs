﻿using System.Collections.Generic;
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
		/// Get profile of current logged in user
		/// </summary>
		/// <param name="params"></param>
		/// <returns>BaseResponse</returns>
		[HttpGet]
		[Produces("application/json")]
		public BaseResponse GetAllConversation([FromHeader]IDictionary<string, string> @params)
		{
			return _conversationService.GetAllConversation(@params);
		}
	}
}