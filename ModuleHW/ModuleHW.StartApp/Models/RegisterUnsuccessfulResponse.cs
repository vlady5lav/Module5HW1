using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class RegisterUnsuccessfulResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
