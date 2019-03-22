using System;
using System.Net;

namespace ChatServer.DAL.Exceptions
{
	public class ConflictException : Exception
	{

		public ConflictException(string message) : base(message)
		{
		}
	}
}
