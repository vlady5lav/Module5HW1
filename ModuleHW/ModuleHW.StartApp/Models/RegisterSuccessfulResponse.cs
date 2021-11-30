using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class RegisterSuccessfulResponse
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
