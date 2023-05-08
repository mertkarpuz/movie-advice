
namespace MovieAdvice.Application.Interfaces
{
    public interface IHttpUtilities
    {
        Task<string> ExecuteGetHttpRequest(string endpoint, Dictionary<string, string> headers);
    }
}
