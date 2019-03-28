using ChatServer.DAL.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ChatServer.DAL.Models
{
	public interface IEntity
	{
	}

	[DataContract]
	public abstract class BaseEntity : IEntity
    {
        [Key]
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DefaultValue(EntityStatus.Activated)]
        [DataMember(Name = "entityStatus")]
        public EntityStatus EntityStatus { get; set; }

        [Required]
        [DataMember(Name = "createdTime")]
        public DateTimeOffset CreatedTime { get; set; }

        [Required]
        [DataMember(Name = "updatedTime")]
        public DateTimeOffset UpdatedTime { get; set; }
    }
}