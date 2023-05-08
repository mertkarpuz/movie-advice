using System.Text.Json.Serialization;

namespace MovieAdvice.Domain.ApiModels.MovieApi
{
    public class DateApiModel
    {
        [JsonPropertyName("maximum")]
        public string? Maximum { get; set; }

        [JsonPropertyName("minimum")]
        public string? Minimum { get; set; }
    }
}
