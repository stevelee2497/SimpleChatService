using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Core.Extensions
{
	public static class DictionaryExtension
	{
		public static string ToUri(this Dictionary<string, string> parameters)
		{
			return parameters != null
				? $"?{string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))}"
				: string.Empty;
		}
	}
}