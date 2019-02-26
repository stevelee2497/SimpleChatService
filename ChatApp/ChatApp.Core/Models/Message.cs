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
}