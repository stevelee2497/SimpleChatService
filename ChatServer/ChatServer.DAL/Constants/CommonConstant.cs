﻿namespace ChatServer.DAL.Constants
{
    public static class CommonConstant
    {
        public class Error
        {
            public const string BadRequest = "Bad Request";
            public const string NotFound = "Not Found";
            public const string InternalServerError = "Internal Server Error";
            public static string BlockedUser = "UserHasBeenBlocked";
	        public static string InvalidInput = "Invalid Input";
		}
    }
}
