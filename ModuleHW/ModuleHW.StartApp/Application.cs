using System.Collections.Generic;
using System.Threading.Tasks;

using ModuleHW.StartApp.Models;
using ModuleHW.StartApp.Services;

namespace ModuleHW.StartApp
{
    public class Application
    {
        private readonly List<Task> _listTask;
        private readonly DataService _dataService;

        public Application(DataService dataService)
        {
            _listTask = new List<Task>();
            _dataService = dataService;
        }

        public async Task Start()
        {
            var createdUser = new UserCreateRequest() { Name = "morpheus", Job = "leader", };
            var updatedUser = new UserUpdateRequest() { Name = "morpheus", Job = "zion resident", };

            var registerUserOk = new RegisterRequest() { Email = "eve.holt@reqres.in", Password = "pistol", };
            var registerUserFail = new RegisterRequest() { Email = "sydney@fife", };

            var loginUserOk = new LoginRequest() { Email = "eve.holt@reqres.in", Password = "cityslicka", };
            var loginUserFail = new LoginRequest() { Email = "peter@klaven", };

            _listTask.Add(_dataService.ReadUserListAsync());
            _listTask.Add(_dataService.ReadUserListAsync(delay: 3));
            _listTask.Add(_dataService.ReadUserListAsync(2));
            _listTask.Add(_dataService.ReadUserListAsync(2, 3));

            _listTask.Add(_dataService.ReadUserAsync(2));
            _listTask.Add(_dataService.ReadUserAsync(2, 3));
            _listTask.Add(_dataService.ReadUserAsync(23));

            _listTask.Add(_dataService.ReadResourceListAsync());
            _listTask.Add(_dataService.ReadResourceListAsync(delay: 3));
            _listTask.Add(_dataService.ReadResourceListAsync(2));
            _listTask.Add(_dataService.ReadResourceListAsync(2, 3));

            _listTask.Add(_dataService.ReadResourceAsync(2));
            _listTask.Add(_dataService.ReadResourceAsync(2, 3));
            _listTask.Add(_dataService.ReadResourceAsync(23));

            _listTask.Add(_dataService.CreateUserAsync(createdUser));
            _listTask.Add(_dataService.UpdatePutUserAsync(2, updatedUser));
            _listTask.Add(_dataService.UpdatePatchUserAsync(2, updatedUser));
            _listTask.Add(_dataService.DeleteUserAsync(2));

            _listTask.Add(_dataService.RegisterUserAsync<RegisterSuccessfulResponse>(registerUserOk));
            _listTask.Add(_dataService.RegisterUserAsync<RegisterUnsuccessfulResponse>(registerUserFail));
            _listTask.Add(_dataService.LoginUserAsync<LoginSuccessfulResponse>(loginUserOk));
            _listTask.Add(_dataService.LoginUserAsync<LoginUnsuccessfulResponse>(loginUserFail));

            await Task.WhenAll(_listTask);
        }
    }
}
