using System.Runtime.Serialization;

namespace ChatServer.BLL.Requests
{
	[DataContract]
	public class UserRequest
	{
		[DataMember(Name = "userId")]
		public string UserId { get; set; }

		[DataMember(Name = "userIndex")]
		public int? UserIndex { get; set; }
	}
}