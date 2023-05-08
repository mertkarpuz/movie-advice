using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.ConfigModels
{
    public class EmailConfiguration
    {
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Client { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string ImageBaseUrl { get; set; }
        public string ImageNotFoundUrl { get; set; }
    }
}
