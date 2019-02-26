using System.Runtime.Serialization;

namespace ChatApp.Core.Models
{
	public class Conversation
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }
	}
}