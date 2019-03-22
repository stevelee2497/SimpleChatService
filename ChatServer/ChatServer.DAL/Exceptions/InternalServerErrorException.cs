using System;
using System.Net;

namespace ChatServer.DAL.Exceptions
{
	public class InternalServerErrorException : Exception
	{
		public InternalServerErrorException(string message) : base(message)
		{
		}
	}
}
