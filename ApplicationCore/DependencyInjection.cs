using ApplicationCore.Common.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ApplicationCore
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                cfg.AddOpenBehavior(typeof(IdempotencyBehavior<,>));
            });
        }
    }
}
