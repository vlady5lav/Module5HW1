using System.Threading.Tasks;

namespace ModuleHW.StartApp.Abstractions
{
    public interface IHttpService
    {
        Task<string> DeleteAsync(string url);

        Task<string> GetStringAsync(string url);

        Task<byte[]> GetBytesAsync(string url);

        Task<string> PatchAsync(string url, object payload);

        Task<string> PostAsync(string url, object payload);

        Task<string> PutAsync(string url, object payload);
    }
}
