using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;

namespace ChatServer.BLL.Services
{
	public interface IUserConversationService : IEntityService<UserConversation>
	{
	}

	public class UserConversationService : EntityService<UserConversation>, IUserConversationService
	{
	}
}