using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTP.WebApi.Data;
using TOTP.WebApi.Models;

namespace TOTP.WebApi.Services
{
    public interface IAuthService
    {
        AuthResponseDto AuthenticateUser(AuthRequestDto request);
    }
    public class AuthService : IAuthService
    {
        public AuthResponseDto AuthenticateUser(AuthRequestDto request)
        {
            var user = UsersDataSource.GetUsers.FirstOrDefault(a => a.Username == request.Username && a.Password == request.Password);
            if (user != null)
            {
                return new AuthResponseDto
                {
                    ClientSecret = user.ClientSecret,
                    IsSuccess = true
                };
            }
            return new AuthResponseDto { IsSuccess = false };
        }
    }
}
