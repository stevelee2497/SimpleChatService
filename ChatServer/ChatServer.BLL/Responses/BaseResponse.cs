using System.Net;

namespace ChatServer.BLL.Responses
{
	public class BaseResponse
	{
		public HttpStatusCode StatusCode { get; set; }

		public string ErrorMessage { get; set; }

		public object Data { get; set; }

		public BaseResponse(HttpStatusCode statusCode, string errorMessage = null, object data = null)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage;
			Data = data;
		}
	}
}