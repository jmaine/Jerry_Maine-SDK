using System.Text.Json.Serialization;

namespace Jerry.Maine.SDK
{
    public class Book
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public string Name { get; set; }

       
    }
}