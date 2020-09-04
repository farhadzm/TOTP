using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTP.WebApi.Utilities
{
    public class ClientSecretGenerator
    {
        public static string CreateClientSecret()
        {
            return Guid.NewGuid().ToString().Replace('-', ' ').Substring(0, 16);
        }
    }
}
