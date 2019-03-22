using System;
using System.Net;

namespace ChatServer.DAL.Exceptions
{
	public class BadRequestException : Exception
	{

		public BadRequestException(string message) : base(message)
		{
		}
	}
}
