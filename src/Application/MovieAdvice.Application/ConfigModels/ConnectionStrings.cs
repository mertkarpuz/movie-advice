﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.ConfigModels
{
    public class ConnectionStrings
    {
        public string SQLConnection { get; set; } = string.Empty;
        public string RedisConnection { get; set; } = string.Empty;
    }
}
