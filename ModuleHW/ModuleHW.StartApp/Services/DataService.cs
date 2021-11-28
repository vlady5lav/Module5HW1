using System;
using System.Collections;
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
                var content = await _httpService?.GetAsync(url);

                if (content == default)
                {
                    return default;
                }

                var response = JsonSerializer.Deserialize<Response<T>>(content);
                var data = response.Data;

                if (data is ICollection)
                {
                    foreach (var t in data as ICollection)
                    {
                        await GetUserAvatarAsync(t);
                    }

                    return data;
                }

                await GetUserAvatarAsync(data);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        public async Task<T> CreatePostAsync<T>(string url, T payload)
        {
            try
            {
                var content = await _httpService?.PostAsync(url, payload);

                if (content == default)
                {
                    return default;
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

        public async Task<string> DeleteAsync(string url)
        {
            try
            {
                var content = await _httpService?.DeleteAsync(url);

                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");

                return default;
            }
        }

        public async Task<T> UpdatePatchAsync<T>(string url, T payload)
        {
            try
            {
                var content = await _httpService?.PatchAsync(url, payload);

                if (content == default)
                {
                    return default;
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

        public async Task<T> UpdatePutAsync<T>(string url, T payload)
        {
            try
            {
                var content = await _httpService?.PutAsync(url, payload);

                if (content == default)
                {
                    return default;
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

        private async Task GetUserAvatarAsync(object u)
        {
            try
            {
                if (u is User)
                {
                    var user = u as User;

                    var avatar = await _httpService?.GetBytesAsync(user?.Avatar);
                    var filePath = $"avatars\\{user?.Avatar[(user.Avatar.LastIndexOf("/") + 1) ..]}";
                    Directory.CreateDirectory("avatars");
                    await File.WriteAllBytesAsync(filePath, avatar);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }
        }
    }
}
