using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;

namespace ChatServer.BLL.Services
{
	public interface IConversationService : IEntityService<Conversation>
	{
	}

	public class ConversationService : EntityService<Conversation>, IConversationService
	{
	}
}