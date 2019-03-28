using System;
using System.Collections.Generic;
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
		/// <param name="params"></param>
		/// <returns>BaseResponse</returns>
		[HttpGet]
		[Produces("application/json")]
		public BaseResponse GetAllUser([FromHeader]IDictionary<string, string> @params)
		{
			return _userService.GetAllUser(@params);
		}

		/// <summary>
		/// Get profile of current logged in user
		/// </summary>
		/// <param name="id"></param>
		/// <returns>BaseResponse</returns>
		[HttpGet("{id}")]
		[Produces("application/json")]
		public BaseResponse ElementAt(int id)
		{
			return _userService.ElementAt(id);
		}
	}
}