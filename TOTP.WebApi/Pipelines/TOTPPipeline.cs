using Microsoft.AspNetCore.Builder;
using TOTP.WebApi.Middlewares;

namespace TOTP.WebApi.Pipelines
{
    public class TOTPPipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<TOTPMiddleware>();
        }
    }
}
