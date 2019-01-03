using Hivemind.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Api.Tests
{
    public class Context
    {
        public object LastResult;
        public object LastError;
        private HttpClient _httpClient;
        private readonly string _baseUrl;

        public Context()
        {
            _httpClient = new HttpClient();
            _baseUrl = ConfigurationManager.AppSettings.Get("webApiPath");
        }

        public void SetTokenHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

        public TResponse Get<TResponse>(string path)
        {
            var url = new Uri(_baseUrl + path);
            var response =  _httpClient.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
            {
                LastError = response;
                return default(TResponse);
            }

            var responseString = response.Content.ReadAsStringAsync().Result;
            LastResult = JsonConvert.DeserializeObject<TResponse>(responseString);
            LastError = null;
            return (TResponse)LastResult;
        }

        public TResponse Post<TRequest, TResponse, TError>(string path, TRequest request)
        {
            var url = new Uri(_baseUrl + path);

            var content = request.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(request, null)?.ToString());

            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(content) };
            var result = _httpClient.SendAsync(req).Result;
            
            var responseString = result.Content.ReadAsStringAsync().Result;
            LastResult = JsonConvert.DeserializeObject<TResponse>(responseString);
            LastError = null;

            return (TResponse)LastResult;
        }

        public TResponse Put<TRequest, TResponse, TError>(string path, TRequest request)
        {
            throw new NotImplementedException();
        }

        public TResponse Delete<TRequest, TResponse, TError>(string path, TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
