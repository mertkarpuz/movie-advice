using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
