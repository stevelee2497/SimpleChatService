using System.Runtime.Serialization;

namespace ChatApp.Core.Models
{
	[DataContract]
	public class User
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "displayName")]
		public string DisplayName { get; set; }

		[DataMember(Name = "avatarUrl")]
		public string AvatarUrl { get; set; }
	}
}