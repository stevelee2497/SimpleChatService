using System.Runtime.Serialization;

namespace ChatServer.BLL.Responses
{
	[DataContract]
	public class MessageResponse
	{
		[DataMember(Name = "userDisplayName")]
		public string UserDisplayName { get; set; }

		[DataMember(Name = "messageContent")]
		public string MessageContent { get; set; }
	}
}