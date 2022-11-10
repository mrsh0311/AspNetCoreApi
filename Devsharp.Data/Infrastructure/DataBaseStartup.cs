using Devsharp.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devsharp.Data.Infrastructure
{
    public class DataBaseStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.AboveNormal;

        public void Configure(IApplicationBuilder app)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddDbContextPool<IApplcationDbContext, SqlServerApplicationContext>(
              c => c.UseSqlServer("Data Source=DESKTOP-V5VQREQ\\SQLEXPRESS;Initial Catalog=ShopG2;" +
              "Integrated Security=true;TrustServerCertificate=True;").UseLazyLoadingProxies()
          , poolSize: 16);
        }
    }
}
