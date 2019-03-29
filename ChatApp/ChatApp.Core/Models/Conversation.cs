using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChatApp.Core.Models
{
	[DataContract]
	public class Conversation
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "users")]
		public List<User> Users { get; set; }

		[DataMember(Name = "messages")]
		public List<Message> Messages { get; set; }
	}
}