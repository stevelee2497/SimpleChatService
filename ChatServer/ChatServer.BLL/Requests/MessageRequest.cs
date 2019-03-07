using System.Runtime.Serialization;

namespace ChatServer.BLL.Requests
{
	[DataContract]
	public class MessageRequest
	{
		[DataMember(Name = "userId")]
		public string UserId { get; set; }

		[DataMember(Name = "conversationId")]
		public string ConversationId { get; set; }

		[DataMember(Name = "userConversationId")]
		public string UserConversationId { get; set; }

		[DataMember(Name = "messageContent")]
		public string MessageContent { get; set; }
	}
}