﻿using System;
using System.Runtime.Serialization;

namespace ChatServer.BLL.Requests
{
	[DataContract]
	public class ConversationRequest
	{
		[DataMember(Name = "userId")]
		public Guid UserId { get; set; }

		[DataMember(Name = "receiverId")]
		public Guid ReceiverId { get; set; }
	}
}