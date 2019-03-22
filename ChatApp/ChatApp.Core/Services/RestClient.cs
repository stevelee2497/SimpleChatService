using ChatApp.Core.Extensions;
using ChatApp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
	public class RestClient
	{
		private const string Bearer = "Bearer";
		private const string ContentType = "application/json";

		public async Task<BaseResponse> Get(string url, string token = null,
			Dictionary<string, string> parameters = null)
		{
			return await SendRequest(RequestMethod.Get, url + parameters.ToUri(), token).ConfigureAwait(false);
		}

		public async Task<BaseResponse> Post(string url, HttpContent content, string token = null,
			Dictionary<string, string> parameters = null)
		{
			return await SendRequest(RequestMethod.Post, url + parameters.ToUri(), token, content)
				.ConfigureAwait(false);
		}

		public async Task<BaseResponse> Put(string url, HttpContent content, string token = null,
			Dictionary<string, string> parameters = null)
		{
			return await SendRequest(RequestMethod.Put, url + parameters.ToUri(), token, content).ConfigureAwait(false);
		}

		public async Task<BaseResponse> Delete(string url, string token = null,
			Dictionary<string, string> parameters = null)
		{
			return await SendRequest(RequestMethod.Delete, url + parameters.ToUri(), token).ConfigureAwait(false);
		}

		private static async Task<BaseResponse> SendRequest(RequestMethod method, string url, string token = null,
			HttpContent content = null)
		{
			try
			{
				HttpResponseMessage httpResponse;

				using (var httpClient = CreateClient(token))
				{
					switch (method)
					{
						case RequestMethod.Get:
							httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false);
							break;
						case RequestMethod.Post:
							httpResponse = await httpClient.PostAsync(url, content).ConfigureAwait(false);
							break;
						case RequestMethod.Put:
							httpResponse = await httpClient.PutAsync(url, content).ConfigureAwait(false);
							break;
						case RequestMethod.Delete:
							httpResponse = await httpClient.DeleteAsync(url).ConfigureAwait(false);
							break;
						default:
							throw new Exception("Request method " + method + " is not supported");
					}
				}

				var response = JsonConvert.DeserializeObject<BaseResponse>(await httpResponse.Content.ReadAsStringAsync());
				response.NetworkStatus = NetworkStatus.Success;
				return response;
			}
			catch (OperationCanceledException)
			{
				return new BaseResponse
				{
					NetworkStatus = NetworkStatus.Timeout,
					ErrorMessage = $"Time out when sending request: {url}"
				};
			}
			catch (Exception e)
			{
				return new BaseResponse
				{
					NetworkStatus = NetworkStatus.Exception,
					ErrorMessage = $"Error: \"{e.Message}\" with request: {url}"
				};
			};
		}

		private static HttpClient CreateClient(string token = null)
		{
			var client = new HttpClient {BaseAddress = new Uri(AppConstants.ApiUrl)};
			client.DefaultRequestHeaders.Add("ContentType", ContentType);
			client.DefaultRequestHeaders.Add("ApiKey", AppConstants.ApiKey);
			client.Timeout = TimeSpan.FromSeconds(AppConstants.ApiTimeout);

			if (token != null)
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Bearer, token);
			}

			return client;
		}
	}
}