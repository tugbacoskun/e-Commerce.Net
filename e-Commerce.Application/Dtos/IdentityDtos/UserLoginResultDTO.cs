using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Dtos.IdentityDtos
{
    public class UserLoginResultDTO
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public TokenDTO Token { get; set; }
    }
}
