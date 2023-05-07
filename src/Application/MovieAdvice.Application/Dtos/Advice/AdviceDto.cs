using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Dtos.Advice
{
    public class AdviceDto
    {
        public int MovieId { get; set; }
        public string ToMailAddress { get; set; }
    }
}
