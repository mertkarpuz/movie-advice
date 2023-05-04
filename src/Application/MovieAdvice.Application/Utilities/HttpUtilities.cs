using MovieAdvice.Application.Interfaces;
using RestSharp;


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
