using System.Net;
using Newtonsoft.Json;

namespace ChatApp.Core.Models
{
	public class BaseResponse
	{
		[JsonIgnore]
		public NetworkStatus NetworkStatus { get; set; }

		public HttpStatusCode StatusCode { get; set; }

		public string ErrorMessage { get; set; }

		public object Data { get; set; }
	}
}