using Domain.Entities.EntitiesBS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataBS
{
    public class SeederBS
    {
        public static async Task SeedAsync(ContextBS bsContext, ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (bsContext.Database.IsSqlServer())
                {
                    bsContext.Database.Migrate();
                }

                if (!await bsContext.Usuarios.AnyAsync())
                {
                    await bsContext.Usuarios.AddRangeAsync(GetPreconfiguredUsers());

                    await bsContext.SaveChangesAsync();
                }

                if (!await bsContext.Estados.AnyAsync())
                {
                    await bsContext.Estados.AddRangeAsync(MakeEstados());

                    await bsContext.SaveChangesAsync();
                }

                if (!await bsContext.Cidades.AnyAsync())
                {
                    await bsContext.Cidades.AddRangeAsync(MakeCidades());

                    await bsContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(bsContext, logger, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<UsuarioBS> GetPreconfiguredUsers()
        {
            return new List<UsuarioBS>
            {
                new("123456","user.demo", "demo123@123.com"),
                new("789100","admin.demo", "demo321@321.com"),
                new("789100","admin.demo", "demo321@321.com", 0),
            };
        }

        static IEnumerable<Estado> MakeEstados()
        {
            return [
                new("uf unknown","Rio de Janeiro")
            ];
        }

        static IEnumerable<Cidade> MakeCidades()
        {
            return [
                new("1","Rio de Janeiro"),
                new("1","Angra dos Reis"),
            ];
        }

    }
}
