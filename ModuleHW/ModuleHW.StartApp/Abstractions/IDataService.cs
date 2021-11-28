using System.Threading.Tasks;

namespace ModuleHW.StartApp.Abstractions
{
    public interface IDataService
    {
        Task<T> DeleteAsync<T>(string url);

        Task<T> GetAsync<T>(string url);

        Task<T> PatchAsync<T>(string url, object payload);

        Task<T> PostAsync<T>(string url, object payload);

        Task<T> PutAsync<T>(string url, object payload);
    }
}
