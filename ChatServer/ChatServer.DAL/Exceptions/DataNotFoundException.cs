using System;
using System.Net;

namespace ChatServer.DAL.Exceptions
{
	public class DataNotFoundException : Exception
	{

		public DataNotFoundException(string entityName) : base(BuildMessage(entityName))
		{
		}

		private static string BuildMessage(string entityName)
			=> $"{entityName}";
	}
}
