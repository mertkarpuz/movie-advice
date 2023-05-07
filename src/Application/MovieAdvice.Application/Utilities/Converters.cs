using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
