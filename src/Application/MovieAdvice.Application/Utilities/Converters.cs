using System.Text;

namespace MovieAdvice.Application.Utilities
{
    public static class Converters
    {
        public static byte[] StringToByteConverter(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
