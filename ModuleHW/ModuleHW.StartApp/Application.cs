using System;
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

        public void Start()
        {
            var createdUser = new User() { Name = "morpheus", Job = "leader", };
            var updatedUser = new User() { Name = "morpheus", Job = "zion resident", };

            var registerOkUser = new User() { Email = "eve.holt@reqres.in", Password = "pistol", };
            var registerFailUser = new User() { Email = "sydney@fife", };
            var loginOkUser = new User() { Email = "eve.holt@reqres.in", Password = "cityslicka", };
            var loginFailUser = new User() { Email = "peter@klaven", };

            _listTask.Add(_dataService.GetAsync<List<User>>(@"https://reqres.in/api/users?page=2"));
            _listTask.Add(_dataService.GetAsync<User>(@"https://reqres.in/api/users/2"));
            _listTask.Add(_dataService.GetAsync<User>(@"https://reqres.in/api/users/23"));

            _listTask.Add(_dataService.GetAsync<List<Resource>>(@"https://reqres.in/api/unknown"));
            _listTask.Add(_dataService.GetAsync<Resource>(@"https://reqres.in/api/unknown/2"));
            _listTask.Add(_dataService.GetAsync<Resource>(@"https://reqres.in/api/unknown/23"));

            _listTask.Add(_dataService.CreatePostAsync(@"https://reqres.in/api/users", createdUser));
            _listTask.Add(_dataService.UpdatePutAsync(@"https://reqres.in/api/users/2", updatedUser));
            _listTask.Add(_dataService.UpdatePatchAsync(@"https://reqres.in/api/users/2", updatedUser));
            _listTask.Add(_dataService.DeleteAsync(@"https://reqres.in/api/users/2"));

            _listTask.Add(_dataService.CreatePostAsync(@"https://reqres.in/api/register", registerOkUser));
            _listTask.Add(_dataService.CreatePostAsync(@"https://reqres.in/api/register", registerFailUser));

            _listTask.Add(_dataService.CreatePostAsync(@"https://reqres.in/api/login", loginOkUser));
            _listTask.Add(_dataService.CreatePostAsync(@"https://reqres.in/api/login", loginFailUser));

            _listTask.Add(_dataService.GetAsync<List<User>>(@"https://reqres.in/api/users?delay=3"));

            Task.WhenAll(_listTask).GetAwaiter().GetResult();

            Console.ReadKey();
        }
    }
}
