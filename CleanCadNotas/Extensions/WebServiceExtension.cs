using ApplicationCore.Interfaces;
using CleanCadNotas.Interfaces;
using CleanCadNotas.Services;
using Infrastructure.IdempotencyServices;

namespace CleanCadNotas.Extensions
{
    public static class WebServiceExtension
    {
        public static void AddWebServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUser, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped <IIdempotencyRepository,IdempotencyRepository > ();
            builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
        }
    }
}
