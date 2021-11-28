using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using ModuleHW.StartApp.Abstractions;

namespace ModuleHW.StartApp.Services
{
    public class HttpService : IHttpService
    {
        public HttpService()
        {
        }

        public async Task<string> DeleteAsync(string url)
        {
            return await RequestHandlerAsync(url, HttpMethod.Delete);
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await RequestHandlerAsync(url, HttpMethod.Get);
        }

        public async Task<byte[]> GetBytesAsync(string url)
        {
            return await GetBytesArrayAsync(url);
        }

        public async Task<string> PatchAsync(string url, object payload)
        {
            return await RequestHandlerAsync(url, HttpMethod.Patch, payload);
        }

        public async Task<string> PostAsync(string url, object payload)
        {
            return await RequestHandlerAsync(url, HttpMethod.Post, payload);
        }

        public async Task<string> PutAsync(string url, object payload)
        {
            return await RequestHandlerAsync(url, HttpMethod.Put, payload);
        }

        private async Task<string> RequestHandlerAsync(string url, HttpMethod httpMethod, object payload = null)
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

                    httpMessage.RequestUri = new Uri($@"{url}");
                    httpMessage.Method = httpMethod;

                    var response = await httpClient.SendAsync(httpMessage);

                    if (response?.StatusCode == HttpStatusCode.OK || response?.StatusCode == HttpStatusCode.Created)
                    {
                        Console.WriteLine($"\nResponse: {(int)response.StatusCode} {response.StatusCode}");

                        return await response.Content?.ReadAsStringAsync();
                    }
                    else if (response?.StatusCode == HttpStatusCode.NoContent || response?.StatusCode == HttpStatusCode.BadRequest || response?.StatusCode == HttpStatusCode.NotFound)
                    {
                        var text = $"Response: {(int)response.StatusCode} {response.StatusCode}";

                        Console.WriteLine($"\n{text}");

                        var result = await response.Content?.ReadAsStringAsync();

                        if (result?.Length < 3)
                        {
                            return text;
                        }

                        return result;
                    }
                    else
                    {
                        var text = $"Response: {(int)response?.StatusCode} {response?.StatusCode}";

                        Console.WriteLine($"\n{text}");

                        var result = await response?.Content?.ReadAsStringAsync();

                        if (result?.Length < 3)
                        {
                            return text;
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        private async Task<byte[]> GetBytesArrayAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    var text = $"\nResponse: {(int)response?.StatusCode} {response?.StatusCode}";

                    Console.WriteLine(text);

                    return default;
                }
            }
        }
    }
}
