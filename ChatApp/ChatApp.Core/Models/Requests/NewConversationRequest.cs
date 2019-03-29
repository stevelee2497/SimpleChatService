using System;
using System.Runtime.Serialization;

namespace ChatApp.Core.Models.Requests
{
	[DataContract]
	public class NewConversationRequest
	{
		[DataMember(Name = "userId")]
		public Guid UserId { get; set; }

		[DataMember(Name = "friendId")]
		public Guid FriendId { get; set; }
	}
}