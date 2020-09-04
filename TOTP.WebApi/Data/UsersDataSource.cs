using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTP.WebApi.Models;
using TOTP.WebApi.Utilities;

namespace TOTP.WebApi.Data
{
    public static class UsersDataSource
    {
        public static IList<Users> GetUsers
        {
            get
            {
                return new List<Users>
                {
                    new Users { Id = 1, Username = "farhad", Password = "123123", ClientSecret = ClientSecretGenerator.CreateClientSecret()  },
                    new Users { Id = 2, Username = "mohammad", Password = "456456", ClientSecret = ClientSecretGenerator.CreateClientSecret()  }
                };
            }
        }
    }
}
