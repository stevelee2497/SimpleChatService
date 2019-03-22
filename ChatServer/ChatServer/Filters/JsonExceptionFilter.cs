using System.Net;
using ChatServer.BLL.Responses;
using ChatServer.DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatServer.Filters
{
	public class JsonExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			HttpStatusCode statusCode;
			switch (context.Exception)
			{
				case BadRequestException _:
					statusCode = HttpStatusCode.BadRequest;
					break;
				case ConflictException _:
					statusCode = HttpStatusCode.Conflict;
					break;
				case DataNotFoundException _:
					statusCode = HttpStatusCode.NotFound;
					break;
				case InternalServerErrorException _:
					statusCode = HttpStatusCode.InternalServerError;
					break;
				default:
					statusCode = HttpStatusCode.ServiceUnavailable;
					break;
			}

			var result = new ObjectResult(new BaseResponse(statusCode, context.Exception.Message)) { StatusCode = (int)statusCode};
			context.Result = result;
		}
	}
}