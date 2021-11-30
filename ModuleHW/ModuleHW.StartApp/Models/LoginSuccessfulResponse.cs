using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class LoginSuccessfulResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
