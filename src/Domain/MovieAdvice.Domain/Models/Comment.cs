using MovieAdvice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieAdvice.Domain.Models
{
    public class Comment : BaseModel
    {
        public int Point { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
