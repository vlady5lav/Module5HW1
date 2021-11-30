using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class UserUpdateRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("job")]
        public string? Job { get; set; }
    }
}
