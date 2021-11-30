using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class RegisterRequest
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
