using System.Threading.Tasks;

namespace ModuleHW.StartApp.Abstractions
{
    public interface IDataService
    {
        Task<T> CreatePostAsync<T>(string url, T payload);
        Task<string> DeleteAsync(string url);
        Task<T> GetAsync<T>(string url);
        Task<T> UpdatePatchAsync<T>(string url, T payload);
        Task<T> UpdatePutAsync<T>(string url, T payload);
    }
}
