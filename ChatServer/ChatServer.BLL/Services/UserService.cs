using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;

//using Giveaway.DataLayers.Models.IntermediateModels;

namespace ChatServer.BLL.Services
{
    public interface IUserService : IEntityService<User>
    {
	    BaseResponse GetAllUser();
    }

    public class UserService : EntityService<User>, IUserService
    {
	    public BaseResponse GetAllUser()
	    {
		    return new BaseResponse(HttpStatusCode.OK, data: All().Select(x => new
		    {
				x.Id,
				x.DisplayName,
				x.AvatarUrl,
		    }));
	    }
    }
}
