using System;
using System.Runtime.Serialization;

namespace ChatServer.BLL.Requests
{
	[DataContract]
	public class BaseRequestParams
	{
		[DataMember(Name = "userId")]
		public Guid UserId { get; set; }

		[DataMember(Name = "index")]
		public int? Index { get; set; }
	}
}