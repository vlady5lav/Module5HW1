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

        public async Task<string> GetAsync(string url)
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

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("\nResponse Status: 200 OK");

                        return await response.Content.ReadAsStringAsync();
                    }
                    else if (response.StatusCode == HttpStatusCode.Created)
                    {
                        Console.WriteLine("\nResponse Status: 201 Created");

                        return await response.Content.ReadAsStringAsync();
                    }
                    else if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        Console.WriteLine("\nResponse Status: 204 No Content");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine("\nResponse Status: 400 Bad Request");
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("\nResponse Status: 404 Not Found");
                    }
                    else
                    {
                        Console.WriteLine($"\nError:\n" +
                            $"Status: {response?.StatusCode}\n" +
                            $"Method: {httpMethod}\n" +
                            $"Url: {url}");
                    }

                    return default;
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

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    Console.WriteLine($"\nError:\n" +
                        $"Status: {response?.StatusCode}\n" +
                        $"Url: {url}");

                    return default;
                }
            }
        }
    }
}
