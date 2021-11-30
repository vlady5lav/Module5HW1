using System;
using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class UserCreateResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("job")]
        public string? Job { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
