using Infrastructure.DataBS;
using Infrastructure.DataCad;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class SeederExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            app.Logger.LogInformation("Seeding Database...");

            using var scope = app.Services.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            try
            {
                var bsContext = scopedProvider.GetRequiredService<ContextBS>();
                await SeederBS.SeedAsync(bsContext, app.Logger);

                var cadContext = scopedProvider.GetRequiredService<ContextCad>();
                await SeederCad.SeederCadnotas(cadContext, app.Logger);

            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "An error occurred seeding the DB.");
            }


        }
    }
}
