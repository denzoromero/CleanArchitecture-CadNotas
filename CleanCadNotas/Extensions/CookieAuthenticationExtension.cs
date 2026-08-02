using CleanCadNotas.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CleanCadNotas.Extensions
{
    public static class CookieAuthenticationExtension
    {
        public const int ValidityMinutesPeriod = 60;
        public const string IdentifierCookieName = "CleanCadNotas";

        public static void AddCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(ValidityMinutesPeriod);
                    options.LogoutPath = "/Home/Logout";
                    options.SlidingExpiration = true;
                    options.Cookie.IsEssential = true;
                    options.EventsType = typeof(RevokeAuthenticationEvents);
                });

            services.AddScoped<RevokeAuthenticationEvents>();

            services.AddAuthorization();
        }
    }
}
