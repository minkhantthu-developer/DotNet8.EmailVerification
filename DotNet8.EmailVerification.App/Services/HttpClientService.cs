using DotNet8.EmailVerification.Shared;
using System.Net.Mime;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DotNet8.EmailVerification.App.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<T> ExecuteAsync<T>(
            string endpoint,
            EnumHttpMethod enumHttpMethod,
            object? obj = null
            )
        {
            HttpResponseMessage response = null;
            HttpContent content = null;
            if (obj is not null)
            {
                var jsonString = obj.ToJson();
                content = new StringContent(jsonString, Encoding.UTF8, Application.Json);
            }
            switch (enumHttpMethod)
            {
                case EnumHttpMethod.GET:
                    response = await _httpClient.GetAsync(endpoint);
                    break;
                case EnumHttpMethod.POST:
                    response = await _httpClient.PostAsync(endpoint, content);
                    break;
                case EnumHttpMethod.PUT:
                    response = await _httpClient.PutAsync(endpoint, content);
                    break;
                case EnumHttpMethod.PATCH:
                    response = await _httpClient.PatchAsync(endpoint, content);
                    break;
                case EnumHttpMethod.DELETE:
                    response = await _httpClient.DeleteAsync(endpoint);
                    break;
                default:
                    throw new ArgumentException("Invalid Method.");
            }
            var jsonStr=await response.Content.ReadAsStringAsync();
            return jsonStr.ToObject<T>()!; 
        }
    }

    public enum EnumHttpMethod
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE
    }
}
