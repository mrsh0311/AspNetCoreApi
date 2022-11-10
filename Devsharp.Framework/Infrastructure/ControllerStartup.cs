using Devsharp.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Devsharp.Framework.Infrastructure.Filters;
using Devsharp.Framwork.Infrastructure.ModelBinders;

namespace Devsharp.Framework.Infrastructure
{
    public class ControllerStartup :IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Low;

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<LogFilterAttribute>();
            IMvcBuilder configController = services.AddControllers(
                option => {

                    var policy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .Build();
                    option.Filters.Add(new AuthorizeFilter(policy));


                    option.Filters.Add(typeof(LogFilterAttribute));
                    option.ModelBinderProviders.Insert(0, new PersianDateEntityBinderProvider());
                }

                );


            services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", options =>
             {
                 options.Authority = "https://localhost:6001";
                 options.RequireHttpsMetadata = false;

                 options.Audience = "ShopApi";

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = "name"

                 };

             });
        }
    }
}
