namespace ChatApp.Core
{
	public enum RequestMethod
	{
		Get,
		Put,
		Post,
		Delete
	}

	public enum NetworkStatus
	{
		Success,
		NoWifi,
		Timeout,
		Exception
	}
}