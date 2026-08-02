using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using Domain.Entities.EntitiesCad.ECadMaterial;
using Domain.Entities.EntitiesCad.ECadObra;
using Domain.Entities.EntitiesCad.ECadProjeto;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using Domain.Entities.EntitiesCad.EObraVSProjeto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataCad
{
    public class SeederCad
    {
        public static async Task SeederCadnotas(ContextCad contextCad,ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (contextCad.Database.IsSqlServer())
                {
                    contextCad.Database.Migrate();
                }

                if (!await contextCad.CadFornecedors.AnyAsync())
                {
                    await contextCad.CadFornecedors.AddRangeAsync(GetPreconfiguredFornecedors());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadTransportadoras.AnyAsync())
                {
                    await contextCad.CadTransportadoras.AddRangeAsync(GetPreconfiguredTransportadoras());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadObras.AnyAsync())
                {
                    await contextCad.CadObras.AddRangeAsync(GetPreconfiguredObras());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadMaterials.AnyAsync())
                {
                    await contextCad.CadMaterials.AddRangeAsync(GetPreconfiguredMaterials());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadProjetos.AnyAsync())
                {
                    await contextCad.CadProjetos.AddRangeAsync(GetPreconfiguredCadProjetos());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.ObraVSProjetos.AnyAsync())
                {
                    await contextCad.ObraVSProjetos.AddRangeAsync(GetPreconfiguredObraVsProjetos());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadDisciplinas.AnyAsync())
                {
                    await contextCad.CadDisciplinas.AddRangeAsync(GetPreconfiguredDisciplinas());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadTipoOCs.AnyAsync())
                {
                    await contextCad.CadTipoOCs.AddRangeAsync(GetPreconfiguredTipoOC());

                    await contextCad.SaveChangesAsync();
                }


                if (!await contextCad.CadMaterialCLVMs.AnyAsync())
                {
                    await contextCad.CadMaterialCLVMs.AddRangeAsync(GetPreconfiguredMaterialCLVM());

                    await contextCad.SaveChangesAsync();
                }

                if (!await contextCad.CadProcedimentos.AnyAsync())
                {
                    await contextCad.CadProcedimentos.AddRangeAsync(GetPreconfiguredProcedimentos());

                    await contextCad.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeederCadnotas(contextCad, logger, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<CadFornecedor> GetPreconfiguredFornecedors()
        {
            return new List<CadFornecedor>
            {
                new("Samsung","samsung","1234","01.01.01"),
                new("Apple","apple","5678","02.02.02")
            };
        }

        static IEnumerable<CadTransportadora> GetPreconfiguredTransportadoras()
        {
            return new List<CadTransportadora>
            {
                new("Fedex", "123", "01.1.1"),
                new("DHL", "312", "02.2.2"),
            };
        }

        static IEnumerable<CadObra> GetPreconfiguredObras()
        {
            return
            [
                new("IT101","Dell"),
                new("HR101", "Psyche")
            ];
        }

        static IEnumerable<CadMaterial> GetPreconfiguredMaterials()
        {
            return
            [
                new("material 1"),
                new("material 2")
            ];
        }

        static IEnumerable<CadProjeto> GetPreconfiguredCadProjetos()
        {
            return [
                new("P-68"),
                new("Project-CLEAN")
            ];
        }

        static IEnumerable<ObraVSProjeto> GetPreconfiguredObraVsProjetos()
        {
            return [
                new(1,1),
                new(2,2)
            ];
        }

        static IEnumerable<CadDisciplina> GetPreconfiguredDisciplinas()
        {
            return [
                new("OE-SAmple"),
                new("AE-Unknown")
            ];
        }

        static IEnumerable<CadTipoOC> GetPreconfiguredTipoOC()
        {
            return [
                new("AE"),
                new("OE")
            ];
        }

        static IEnumerable<CadMaterialCLVM> GetPreconfiguredMaterialCLVM()
        {
            return [
                new("BB-80.01.02.73681"),
                new("BB-80.01.02.123456")
            ];
        }

        static IEnumerable<CadProcedimento> GetPreconfiguredProcedimentos()
        {
            return [
                new("SO-01"),
                new("PRO-02"),
                CadProcedimento.Create("sample", 1, null)
            ];
        }





    }
}
