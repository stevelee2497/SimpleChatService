using ChatServer.BLL.Requests;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
	[Route("api/messages")]
	public class MessageController
	{
		private readonly IMessageService _messageService;

		public MessageController(IMessageService messageService)
		{
			_messageService = messageService;
		}

		/// <summary>
		/// Create a new message
		/// </summary>
		/// <param name="messageRequest"></param>
		/// <returns>BaseResponse</returns>
		[HttpPost]
		[Produces("application/json")]
		public BaseResponse Create([FromBody]MessageRequest messageRequest)
		{
			return _messageService.Create(messageRequest);
		}
	}
}