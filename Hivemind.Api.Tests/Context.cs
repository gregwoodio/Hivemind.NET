using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            _baseUrl = Properties.Resource.webApiPath;
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
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_baseUrl + path, content).Result;

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
