using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModuleHW.StartApp.Services
{
    public class HttpService
    {
        private readonly string _apiUrl;

        public HttpService()
        {
            _apiUrl = "https://reqres.in/api/";
        }

        public async Task<string> DeleteAsync(string requestUrl)
        {
            return await RequestHandlerAsync(requestUrl, HttpMethod.Delete);
        }

        public async Task<string> GetStringAsync(string requestUrl)
        {
            return await RequestHandlerAsync(requestUrl, HttpMethod.Get);
        }

        public async Task<byte[]> GetBytesArrayAsync(string requestUrl)
        {
            return await GetBytesAsync(requestUrl);
        }

        public async Task<string> PatchAsync(string requestUrl, object payload)
        {
            return await RequestHandlerAsync(requestUrl, HttpMethod.Patch, payload);
        }

        public async Task<string> PostAsync(string requestUrl, object payload)
        {
            return await RequestHandlerAsync(requestUrl, HttpMethod.Post, payload);
        }

        public async Task<string> PutAsync(string requestUrl, object payload)
        {
            return await RequestHandlerAsync(requestUrl, HttpMethod.Put, payload);
        }

        private async Task<string> RequestHandlerAsync(string requestUrl, HttpMethod httpMethod, object payload = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var httpMessage = new HttpRequestMessage();

                    if (payload != null)
                    {
                        httpMessage.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                    }

                    httpMessage.RequestUri = new Uri($@"{_apiUrl}{requestUrl}");
                    httpMessage.Method = httpMethod;

                    var response = await httpClient.SendAsync(httpMessage);

                    var status = $"Response: {(int)response?.StatusCode} {response?.StatusCode}";

                    Console.WriteLine($"\n{status ?? "No Response! :("}");

                    var result = await response?.Content?.ReadAsStringAsync();

                    if (result != default && result.Trim().StartsWith("{") && result.Trim().Length > 2)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"\nContent of \"{httpMessage.RequestUri}\": \"{result ?? "No Result! :("}\"");

                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        private async Task<byte[]> GetBytesAsync(string requestUrl)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(requestUrl);

                    var status = $"Response: {(int)response?.StatusCode} {response?.StatusCode}";

                    Console.WriteLine($"\n{status ?? "No Response :("}");

                    var result = await response?.Content?.ReadAsByteArrayAsync();

                    return result ?? default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }
    }
}
