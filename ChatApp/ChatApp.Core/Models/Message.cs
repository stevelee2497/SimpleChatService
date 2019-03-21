using System.Runtime.Serialization;

namespace ChatApp.Core.Models
{
	[DataContract]
	public class Message
	{
		public string MessageContent { get; set; }

		public User User { get; set; }

		public Conversation Conversation { get; set; }
	}

	[DataContract]
	public class MessageResponse
	{
		[DataMember(Name = "userDisplayName")]
		public string UserDisplayName { get; set; }

		[DataMember(Name = "messageContent")]
		public string MessageContent { get; set; }
	}

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