using ApplicationCore.Interfaces;
using Infrastructure.DataBS;
using Infrastructure.DataCad;
using Infrastructure.Interceptors;
using Infrastructure.LoggerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
            services.AddScoped<IAuditLogger, ApplicationAuditLogger>();

            if (environment.IsDevelopment())
            {
                services.AddDbContext<ContextBS>((provider, options) => options.UseInMemoryDatabase("BS"));
                //services.AddDbContext<ContextCad>((provider, options) =>options.UseInMemoryDatabase("Cad"));

                services.AddDbContext<ContextCad>((provider, options) =>
                {
                    options.UseInMemoryDatabase("Cad");
                    options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
                });
            }
            else
            {

            }

            //services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IRepositoryBS<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepositoryCad<>), typeof(EfRepositoryCad<>));

            services.AddScoped<IContextBS>(provider => provider.GetRequiredService<ContextBS>());
            services.AddScoped<IContextCad>(provider => provider.GetRequiredService<ContextCad>());

        }
    }
}
