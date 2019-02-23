using System;

namespace ChatServer.DAL.Exceptions
{
	public class InternalServerErrorException : Exception
	{
		public InternalServerErrorException(string message) : base(message)
		{
		}
	}
}
