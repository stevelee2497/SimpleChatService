using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ChatServer.DAL.Models
{
	[Table("Conversation")]
	[DataContract]
	public class Conversation : BaseEntity
	{
		public virtual List<UserConversation> UserConversations { get; set; }

		[NotMapped]
		[DataMember(Name = "userConversationId")]
		public Guid UserConversationId { get; set; }

		[NotMapped]
		[DataMember(Name = "users")]
		public IEnumerable<User> Users { get; set; }

		[NotMapped]
		[DataMember(Name = "messages")]
		public IEnumerable<Message> Messages { get; set; }
	}
}