using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MovieAdvice.Domain.ApiModels.MovieApi;

namespace MovieAdvice.Domain.ApiModels
{
    public class RootApiModel
    {
        [JsonPropertyName("dates")]
        public DateApiModel? Date { get; set; }

        [JsonPropertyName("page")]
        public int? Page { get; set; }

        [JsonPropertyName("results")]
        public List<MovieApiModel>? Movies { get; set; }

        [JsonPropertyName("total_pages")]
        public int? TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int? TotalResults { get; set; }
    }
}
