using System;

namespace ChatServer.DAL.Models
{
	public class Connection : BaseEntity
	{
		public string UserAgent { get; set; }
		public bool Connected { get; set; }
		public Guid? UserId { get; set; }
		public virtual User User { get; set; }
	}
}