using ChatServer.BLL.Responses;
using ChatServer.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
	[Route("api/users")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		/// <inheritdoc />
		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		/// <summary>
		/// Get profile of current logged in user
		/// </summary>
		/// <returns>UserProfileResponse</returns>
		[HttpGet]
		[Produces("application/json")]
		public BaseResponse GetAllUser()
		{
			return _userService.GetAllUser();
		}
	}
}