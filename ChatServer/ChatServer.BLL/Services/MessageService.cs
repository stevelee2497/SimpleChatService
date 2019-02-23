using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;

namespace ChatServer.BLL.Services
{
	public interface IMessageService : IEntityService<Message>
	{
	}

	public class MessageService : EntityService<Message>, IMessageService
	{
	}
}