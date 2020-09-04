using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TOTP.WebApi.Models;
using TOTP.WebApi.Utilities;

namespace TOTP.WebApi.Middlewares
{
    public class TOTPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;

        public TOTPMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }
        private const string requestKey = "Request-Key";
        private const string clientSecretHeader = "ClientSecret";
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey(requestKey) || !httpContext.Request.Headers.ContainsKey(clientSecretHeader))
            {
                await WriteToReponseAsync();
                return;
            }

            var requestKeyHeader = httpContext.Request.Headers[requestKey].ToString();
            string clientSecret = httpContext.Request.Headers[clientSecretHeader].ToString();
            if (string.IsNullOrEmpty(requestKeyHeader) || string.IsNullOrEmpty(clientSecret))
            {
                await WriteToReponseAsync();
                return;
            }
            //اگر کلید در کش موجود بود یعنی کاربر از کلید تکراری استفاده کرده است
            if (_cache.GetString(requestKeyHeader) != null)
            {
                await WriteToReponseAsync();
                return;
            }
            var dateTimeNow = DateTime.UtcNow;
            var expireTimeFrom = dateTimeNow.AddMinutes(-1).Ticks;
            var expireTimeTo = dateTimeNow.Ticks;

            string decryptedRequestHeader = AesProvider.Decrypt(requestKeyHeader, clientSecret);
            var requestKeyData = System.Text.Json.JsonSerializer.Deserialize<TOTPRequestDto>(decryptedRequestHeader);

            if (requestKeyData.DateTimeUtcTicks >= expireTimeFrom && requestKeyData.DateTimeUtcTicks <= expireTimeTo)
            {
                //ذخیره کلید درخواست در کش برای جلوگیری از استفاده مجدد از کلید
                await _cache.SetAsync(requestKeyHeader, Encoding.UTF8.GetBytes("KeyExist"), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2)
                });
                await _next(httpContext);
            }
            else
            {
                await WriteToReponseAsync();
                return;
            }

            async Task WriteToReponseAsync()
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await httpContext.Response.WriteAsync("Forbidden: You don't have permission to call this api");
            }
        }
    }
}
