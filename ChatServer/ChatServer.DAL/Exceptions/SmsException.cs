using System;

namespace ChatServer.DAL.Exceptions
{
	public class SmsException : Exception
	{
		public SmsException(string message) : base(message)
		{
		}
	}
}
