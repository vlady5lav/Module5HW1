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
            var createdUser = new UserAlt() { Name = "morpheus", Job = "leader", };
            var updatedUser = new UserAlt() { Name = "morpheus", Job = "zion resident", };

            var registerOkUser = new Login() { Email = "eve.holt@reqres.in", Password = "pistol", };
            var registerFailUser = new Login() { Email = "sydney@fife", };

            var loginOkUser = new Login() { Email = "eve.holt@reqres.in", Password = "cityslicka", };
            var loginFailUser = new Login() { Email = "peter@klaven", };

            _listTask.Add(_dataService.GetAsync<List<User>>(@"https://reqres.in/api/users?page=2"));

            _listTask.Add(_dataService.GetAsync<User>(@"https://reqres.in/api/users/2"));
            _listTask.Add(_dataService.GetAsync<string>(@"https://reqres.in/api/users/23"));

            _listTask.Add(_dataService.GetAsync<List<Resource>>(@"https://reqres.in/api/unknown"));

            _listTask.Add(_dataService.GetAsync<Resource>(@"https://reqres.in/api/unknown/2"));
            _listTask.Add(_dataService.GetAsync<string>(@"https://reqres.in/api/unknown/23"));

            _listTask.Add(_dataService.PostAsync<UserAlt>(@"https://reqres.in/api/users", createdUser));

            _listTask.Add(_dataService.PutAsync<UserAlt>(@"https://reqres.in/api/users/2", updatedUser));

            _listTask.Add(_dataService.PatchAsync<UserAlt>(@"https://reqres.in/api/users/2", updatedUser));

            _listTask.Add(_dataService.DeleteAsync<string>(@"https://reqres.in/api/users/2"));

            _listTask.Add(_dataService.PostAsync<AuthToken>(@"https://reqres.in/api/register", registerOkUser));
            _listTask.Add(_dataService.PostAsync<AuthToken>(@"https://reqres.in/api/register", registerFailUser));

            _listTask.Add(_dataService.PostAsync<AuthToken>(@"https://reqres.in/api/login", loginOkUser));
            _listTask.Add(_dataService.PostAsync<AuthToken>(@"https://reqres.in/api/login", loginFailUser));

            _listTask.Add(_dataService.GetAsync<List<User>>(@"https://reqres.in/api/users?delay=3"));

            await Task.WhenAll(_listTask);
        }
    }
}
