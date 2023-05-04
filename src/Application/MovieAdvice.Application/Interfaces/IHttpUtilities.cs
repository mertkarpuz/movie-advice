using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Interfaces
{
    public interface IHttpUtilities
    {
        Task<string?> ExecuteGetHttpRequest(string endpoint, Dictionary<string, string>? headers);
    }
}
