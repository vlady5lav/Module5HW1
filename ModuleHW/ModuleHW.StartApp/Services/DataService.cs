using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using ModuleHW.StartApp.Abstractions;
using ModuleHW.StartApp.Models;

namespace ModuleHW.StartApp.Services
{
    public class DataService : IDataService
    {
        private readonly HttpService _httpService;

        public DataService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var content = await _httpService?.GetStringAsync(url);

                if (content == default || !content.Trim().StartsWith("{"))
                {
                    return (T)(object)content;
                }

                var response = JsonSerializer.Deserialize<Response<T>>(content);
                var data = response.Data;

                if (data is ICollection<User>)
                {
                    foreach (var user in data as ICollection<User>)
                    {
                        await GetUserAvatarAsync(user);
                    }

                    return data;
                }

                if (data is User)
                {
                    await GetUserAvatarAsync(data as User);
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        public async Task<T> PostAsync<T>(string url, object payload)
        {
            try
            {
                var content = await _httpService?.PostAsync(url, payload);

                if (content == default || !content.Trim().StartsWith("{"))
                {
                    return (T)(object)content;
                }

                var response = JsonSerializer.Deserialize<T>(content);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            try
            {
                var content = await _httpService?.DeleteAsync(url);

                if (content == default || !content.Trim().StartsWith("{"))
                {
                    return (T)(object)content;
                }

                var response = JsonSerializer.Deserialize<T>(content);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        public async Task<T> PatchAsync<T>(string url, object payload)
        {
            try
            {
                var content = await _httpService?.PatchAsync(url, payload);

                if (content == default || !content.Trim().StartsWith("{"))
                {
                    return (T)(object)content;
                }

                var response = JsonSerializer.Deserialize<T>(content);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        public async Task<T> PutAsync<T>(string url, object payload)
        {
            try
            {
                var content = await _httpService?.PutAsync(url, payload);

                if (content == default || !content.Trim().StartsWith("{"))
                {
                    return (T)(object)content;
                }

                var response = JsonSerializer.Deserialize<T>(content);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        private async Task GetUserAvatarAsync(User user)
        {
            try
            {
                var avatar = await _httpService?.GetBytesAsync(user?.Avatar);
                var filePath = $"avatars\\{user?.Avatar[(user.Avatar.LastIndexOf("/") + 1) ..]}";
                Directory.CreateDirectory("avatars");
                await File.WriteAllBytesAsync(filePath, avatar);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }
        }
    }
}
