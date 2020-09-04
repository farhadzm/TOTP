using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTP.WebApi.Models
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ClientSecret { get; set; }
    }
}
