using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using ModuleHW.StartApp.Abstractions;
using ModuleHW.StartApp.Models;

namespace ModuleHW.StartApp.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpService _httpService;

        public ApiService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserCreateResponse> CreateUserAsync(object payload)
        {
            try
            {
                var content = await _httpService?.PostAsync("users", payload);

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<UserCreateResponse>(content);

                    return response;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<User> ReadUserAsync(int id, int? delay = null)
        {
            try
            {
                var content = await _httpService?.GetStringAsync(delay == null ? $"users/{id}" : $"users/{id}?delay={delay}");

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<ApiResponse<User>>(content);
                    var user = response?.Data;
                    if (user != default)
                    {
                        await GetUserAvatarAsync(user);
                        return user;
                    }
                    else
                    {
                        return default;
                    }
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<Resource> ReadResourceAsync(int id, int? delay = null)
        {
            try
            {
                var content = await _httpService?.GetStringAsync(delay == null ? $"unknown/{id}" : $"unknown/{id}?delay={delay}");

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<ApiResponse<Resource>>(content);
                    var resource = response?.Data;
                    return resource;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<List<User>> ReadUserListAsync(int? page = null, int? delay = null)
        {
            try
            {
                var content = await _httpService?
                    .GetStringAsync(
                        page == null ?
                        delay == null ? $"users" : $"users?delay={delay}" :
                        delay == null ? $"users?page={page}" : $"users?page={page}?delay={delay}");

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<ApiResponse<List<User>>>(content);
                    var listUsers = response?.Data;

                    foreach (var user in listUsers)
                    {
                        if (user != default)
                        {
                            await GetUserAvatarAsync(user);
                        }
                    }

                    return listUsers ?? default;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<List<Resource>> ReadResourceListAsync(int? page = null, int? delay = null)
        {
            try
            {
                var content = await _httpService?
                    .GetStringAsync(
                        page == null ?
                        delay == null ? $"unknown" : $"unknown?delay={delay}" :
                        delay == null ? $"unknown?page={page}" : $"unknown?page={page}delay={delay}");

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<ApiResponse<List<Resource>>>(content);
                    var listResources = response?.Data;
                    return listResources;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<UserUpdateResponse> UpdatePatchUserAsync(int id, object payload)
        {
            try
            {
                var content = await _httpService?.PatchAsync($"users/{id}", payload);

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<UserUpdateResponse>(content);

                    return response;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<UserUpdateResponse> UpdatePutUserAsync(int id, object payload)
        {
            try
            {
                var content = await _httpService?.PutAsync($"users/{id}", payload);

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<UserUpdateResponse>(content);

                    return response;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var content = await _httpService?.DeleteAsync($"users/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");
            }
        }

        public async Task<T> LoginUserAsync<T>(object payload)
        {
            try
            {
                var content = await _httpService?.PostAsync("login", payload);

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<T>(content);

                    return response;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        public async Task<T> RegisterUserAsync<T>(object payload)
        {
            try
            {
                var content = await _httpService?.PostAsync("register", payload);

                if (content != default && content.Trim().StartsWith("{") && content.Trim().Length > 2)
                {
                    var response = JsonSerializer.Deserialize<T>(content);

                    return response;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");

                return default;
            }
        }

        private async Task GetUserAvatarAsync(User user)
        {
            try
            {
                if (user != default)
                {
                    var avatar = await _httpService?.GetBytesArrayAsync(user?.Avatar);
                    var filePath = $@"avatars\{user?.Avatar[(user.Avatar.LastIndexOf("/") + 1)..]}";
                    Directory.CreateDirectory("avatars");
                    await File.WriteAllBytesAsync(filePath, avatar);
                }
                else
                {
                    Console.WriteLine($"\nGetUserAvatarAsync: \"User is default\"!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex?.Message ?? "Unknown exception!"}");
            }
        }
    }
}
