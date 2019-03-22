using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatServer.DAL.Models
{
    [Table("Conversation")]
    public class Conversation : BaseEntity
    {
        public virtual List<UserConversation> UserConversations { get; set; }
    }
}