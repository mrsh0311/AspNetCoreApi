using Devsharp.Core.Extenstions;
using Devsharp.Core.Infrastructure;

using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Devsharp.Framework.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            var ListTypes = typeof(IApplicationStartup).GetAllClassTypes();


            List<IApplicationStartup> ListObjects = new List<IApplicationStartup>();

            foreach (var Typeitem in ListTypes)
            {
                var ob = Activator.CreateInstance(Typeitem) as IApplicationStartup;
                ListObjects.Add(ob);
            }

            foreach (var item in ListObjects.OrderBy(p => p.Priority))
            {
                item.Configure(application);
            }



        }
    }

}
