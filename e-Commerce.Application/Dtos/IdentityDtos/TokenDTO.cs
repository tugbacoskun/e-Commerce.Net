﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Dtos.IdentityDtos
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
