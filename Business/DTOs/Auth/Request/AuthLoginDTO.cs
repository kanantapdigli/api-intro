﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Auth.Request
{
    public class AuthLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
