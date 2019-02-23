using System;
using System.Net;
using System.Text.RegularExpressions;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;

//using Giveaway.DataLayers.Models.IntermediateModels;

namespace ChatServer.BLL.Services
{
    public interface IUserService : IEntityService<User>
    {
    }

    public class UserService : EntityService<User>, IUserService
    {
    }
}
