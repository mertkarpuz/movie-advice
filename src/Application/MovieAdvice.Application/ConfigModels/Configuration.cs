using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.ConfigModels
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new();
        public MovieApiConfigurations MovieApiConfigurations { get; set; } = new();
    }
}
