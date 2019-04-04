using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ChatServer.DAL.Models
{
    [Table("Message")]
	[DataContract]
    public class Message : BaseEntity
    {
		[DataMember(Name = "userConversationId")]
        public Guid? UserConversationId { get; set; }

        [Required]
		[DataMember(Name = "messageContent")]
        public string MessageContent { get; set; }

        public virtual UserConversation UserConversation { get; set; }

		[NotMapped]
		[DataMember(Name = "userId")]
		public Guid? UserId { get; set; }

		[NotMapped]
		[DataMember(Name = "displayName")]
	    public string DisplayName { get; set; }

		[NotMapped]
		[DataMember(Name = "avatarUrl")]
	    public string AvatarUrl { get; set; }
    }
}