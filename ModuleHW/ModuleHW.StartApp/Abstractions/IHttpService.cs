using System.Threading.Tasks;

namespace ModuleHW.StartApp.Abstractions
{
    public interface IHttpService
    {
        Task<string> DeleteAsync(string requestUrl);

        Task<byte[]> GetBytesArrayAsync(string requestUrl);

        Task<string> GetStringAsync(string requestUrl);

        Task<string> PatchAsync(string requestUrl, object payload);

        Task<string> PostAsync(string requestUrl, object payload);

        Task<string> PutAsync(string requestUrl, object payload);
    }
}
