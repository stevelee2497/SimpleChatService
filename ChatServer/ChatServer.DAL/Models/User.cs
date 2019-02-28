using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ChatServer.DAL.Models
{
	[Table("User")]
    [DataContract]
    public class User : BaseEntity
    {
        #region Required Properties

		[DataMember(Name = "displayName")]
	    [MaxLength(40)]
	    public string DisplayName { get; set; }

	    [DataMember(Name = "avatarUrl")]
	    public string AvatarUrl { get; set; }

		#endregion

	    public virtual ICollection<Connection> Connections { get; set; }
	    public virtual ICollection<UserConversation> UserConversations { get; set; }
    }
}
