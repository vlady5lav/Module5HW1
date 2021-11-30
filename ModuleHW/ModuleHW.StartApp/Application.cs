using System.Collections.Generic;
using System.Threading.Tasks;

using ModuleHW.StartApp.Abstractions;
using ModuleHW.StartApp.Models;

namespace ModuleHW.StartApp
{
    public class Application
    {
        private readonly List<Task> _listTask;
        private readonly IApiService _apiService;

        public Application(IApiService apiService)
        {
            _listTask = new List<Task>();
            _apiService = apiService;
        }

        public async Task Start()
        {
            var createdUser = new UserCreateRequest() { Name = "morpheus", Job = "leader", };
            var updatedUser = new UserUpdateRequest() { Name = "morpheus", Job = "zion resident", };

            var registerUserOk = new RegisterRequest() { Email = "eve.holt@reqres.in", Password = "pistol", };
            var registerUserFail = new RegisterRequest() { Email = "sydney@fife", };

            var loginUserOk = new LoginRequest() { Email = "eve.holt@reqres.in", Password = "cityslicka", };
            var loginUserFail = new LoginRequest() { Email = "peter@klaven", };

            _listTask.Add(_apiService.ReadUserListAsync());
            _listTask.Add(_apiService.ReadUserListAsync(delay: 3));
            _listTask.Add(_apiService.ReadUserListAsync(2));
            _listTask.Add(_apiService.ReadUserListAsync(2, 3));

            _listTask.Add(_apiService.ReadUserAsync(2));
            _listTask.Add(_apiService.ReadUserAsync(2, 3));
            _listTask.Add(_apiService.ReadUserAsync(23));

            _listTask.Add(_apiService.ReadResourceListAsync());
            _listTask.Add(_apiService.ReadResourceListAsync(delay: 3));
            _listTask.Add(_apiService.ReadResourceListAsync(2));
            _listTask.Add(_apiService.ReadResourceListAsync(2, 3));

            _listTask.Add(_apiService.ReadResourceAsync(2));
            _listTask.Add(_apiService.ReadResourceAsync(2, 3));
            _listTask.Add(_apiService.ReadResourceAsync(23));

            _listTask.Add(_apiService.CreateUserAsync(createdUser));
            _listTask.Add(_apiService.UpdatePutUserAsync(2, updatedUser));
            _listTask.Add(_apiService.UpdatePatchUserAsync(2, updatedUser));
            _listTask.Add(_apiService.DeleteUserAsync(2));

            _listTask.Add(_apiService.RegisterUserAsync<RegisterSuccessfulResponse>(registerUserOk));
            _listTask.Add(_apiService.RegisterUserAsync<RegisterUnsuccessfulResponse>(registerUserFail));
            _listTask.Add(_apiService.LoginUserAsync<LoginSuccessfulResponse>(loginUserOk));
            _listTask.Add(_apiService.LoginUserAsync<LoginUnsuccessfulResponse>(loginUserFail));

            await Task.WhenAll(_listTask);
        }
    }
}
