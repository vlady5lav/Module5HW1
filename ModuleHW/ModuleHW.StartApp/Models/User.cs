using System.Text.Json.Serialization;

namespace ModuleHW.StartApp.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
    }
}
