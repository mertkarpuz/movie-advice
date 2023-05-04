using MovieAdvice.Application.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Utilities
{
    public class HttpUtilities : IHttpUtilities
    {
        public async Task<string?> ExecuteGetHttpRequest(string endpoint, Dictionary<string, string>? headers)
        {
            RestClient client = new(endpoint);
            RestRequest request = new(endpoint, Method.Get);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
            RestResponse? response = await client.ExecuteAsync(request);

            return response.Content;
        }
    }
}
