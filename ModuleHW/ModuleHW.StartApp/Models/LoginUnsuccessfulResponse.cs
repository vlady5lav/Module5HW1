using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class LoginUnsuccessfulResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
