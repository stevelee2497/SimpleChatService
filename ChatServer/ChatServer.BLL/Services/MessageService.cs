using System;
using System.Net;
using ChatServer.BLL.Requests;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Exceptions;
using ChatServer.DAL.Models;

namespace ChatServer.BLL.Services
{
	public interface IMessageService : IEntityService<Message>
	{
		BaseResponse Create(MessageRequest messageRequest);
	}

	public class MessageService : EntityService<Message>, IMessageService
	{
		public BaseResponse Create(MessageRequest messageRequest)
		{
			bool isSaved;
			try
			{
				Create(new Message
				{
					UserConversationId = Guid.Parse(messageRequest.UserConversationId),
					MessageContent = messageRequest.MessageContent
				}, out isSaved);
			}
			catch (Exception e)
			{
				throw new BadRequestException(e.Message);
			}

			if (!isSaved)
			{
				throw new InternalServerErrorException("Ops, Something went wrong");
			}

			return new BaseResponse(HttpStatusCode.OK, data: "Created");
		}
	}
}