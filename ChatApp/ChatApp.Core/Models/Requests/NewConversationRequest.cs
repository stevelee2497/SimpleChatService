using System;
using System.Runtime.Serialization;

namespace ChatApp.Core.Models.Requests
{
	[DataContract]
	public class NewConversationRequest
	{
		[DataMember(Name = "userId")]
		public string UserId { get; set; }

		[DataMember(Name = "friendId")]
		public string FriendId { get; set; }
	}
}