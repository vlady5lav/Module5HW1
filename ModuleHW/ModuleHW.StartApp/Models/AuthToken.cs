using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class AuthToken
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
