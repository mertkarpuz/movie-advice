
namespace MovieAdvice.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetData<T>(string key);
        Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);
        Task<bool> RemoveData(string key);
    }
}
