using System.Runtime.Serialization;

namespace ChatApp.Core.Models
{
	[DataContract]
	public class Message
	{
		[DataMember(Name = "Id")]
		public string Id { get; set; }

		[DataMember(Name = "userId")]
		public string UserId { get; set; }

		[DataMember(Name = "conversationId")]
		public string ConversationId { get; set; }

		[DataMember(Name = "userConversationId")]
		public string UserConversationId { get; set; }

		[DataMember(Name = "messageContent")]
		public string MessageContent { get; set; }

		[DataMember(Name = "displayName")]
		public string DisplayName { get; set; }

		[DataMember(Name = "avatarUrl")]
		public string AvatarUrl { get; set; }

		[DataMember(Name = "createdTime")]
		public string CreatedTime { get; set; }
	}
}