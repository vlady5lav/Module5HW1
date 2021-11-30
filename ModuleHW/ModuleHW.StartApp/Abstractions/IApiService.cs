using System.Collections.Generic;
using System.Threading.Tasks;

using ModuleHW.StartApp.Models;

namespace ModuleHW.StartApp.Abstractions
{
    public interface IApiService
    {
        Task<UserCreateResponse> CreateUserAsync(object payload);

        Task DeleteUserAsync(int id);

        Task<T> LoginUserAsync<T>(object payload);

        Task<Resource> ReadResourceAsync(int id, int? delay = null);

        Task<List<Resource>> ReadResourceListAsync(int? page = null, int? delay = null);

        Task<User> ReadUserAsync(int id, int? delay = null);

        Task<List<User>> ReadUserListAsync(int? page = null, int? delay = null);

        Task<T> RegisterUserAsync<T>(object payload);

        Task<UserUpdateResponse> UpdatePatchUserAsync(int id, object payload);

        Task<UserUpdateResponse> UpdatePutUserAsync(int id, object payload);
    }
}
