using System;
using ChatServer.BLL.Responses;
using ChatServer.BLL.Services.Base;
using ChatServer.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ChatServer.BLL.Extensions;
using ChatServer.BLL.Requests;

//using Giveaway.DataLayers.Models.IntermediateModels;

namespace ChatServer.BLL.Services
{
	public interface IUserService : IEntityService<User>
    {
	    BaseResponse GetAllUser(IDictionary<string, string> @params);
	    BaseResponse ElementAt(int id);
    }

    public class UserService : EntityService<User>, IUserService
    {
	    public BaseResponse GetAllUser(IDictionary<string, string> @params)
	    {
		    var users = All();

		    users = FilterUserWithParams(users, @params);

			return new BaseResponse(HttpStatusCode.OK, data: users.Select(x => new
		    {
				x.Id,
				x.DisplayName,
				x.AvatarUrl,
		    }));
	    }

	    public BaseResponse ElementAt(int id)
	    {
		    return new BaseResponse(HttpStatusCode.OK, data:All().ToList().ElementAt(id));
	    }

	    private IQueryable<User> FilterUserWithParams(IQueryable<User> users, IDictionary<string, string> @params)
	    {
		    var requestParams = @params.ToObject<UserRequest>();

		    // filter request with param @index
		    // get all friends of user at index = @index
		    if (requestParams.UserIndex != null)
		    {
			    var user = users.ToList().ElementAt(Convert.ToInt32(requestParams.UserIndex));
			    users = users.Where(x => x.Id != user.Id);
		    }

		    // filter request with param @index
		    // get all friends of user at index = @index
		    if (requestParams.UserId != null)
		    {
			    var userId = Guid.Parse(requestParams.UserId);
			    users = users.Where(x => x.Id != userId);
		    }

		    return users;
	    }
    }
}
