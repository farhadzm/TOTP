using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTP.WebApi.Models
{
    public class TOTPRequestDto
    {
        public long DateTimeUtcTicks { get; set; }
    }
}
