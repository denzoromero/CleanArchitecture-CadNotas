using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.CadNotasCore.Projetos;
using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Specifications
{
    public class SearchNotaFiscaisSpecification : Specification<EntityLVM, NotaFiscaisVM>
    {
        public static Expression<Func<EntityLVM, bool>> BuildFilter(string? filter, int ativo, int idObra)
        {
            return i => (string.IsNullOrEmpty(filter)
                                                            || (i.MascaraLVM != null && i.MascaraLVM.Contains(filter))
                                                            || (i.NotaFiscal != null && i.NotaFiscal.Contains(filter))
                                                            || (i.Documento != null && i.Documento.Contains(filter))
                                                            || (i.OC != null && i.OC.Contains(filter))
                                                            || (i.RM != null && i.RM.Contains(filter))
                                                            || (i.Obs != null && i.Obs.Contains(filter))
                                                            || (i.RC != null && i.RC.Contains(filter))
                                                             )
                                                            && i.IdObra == idObra
                                                             && i.Ativo == ativo;
        }

        public SearchNotaFiscaisSpecification(string? filter, int ativo, int idObra, int pageNo = 0)
        {

            Query.AsNoTracking().Where(BuildFilter(filter, ativo, idObra))
                      .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE)
                .Select(x => new NotaFiscaisVM
                {
                    Id = x.Id,
                    IdObra = x.IdObra,
                    MascaraLVM = x.MascaraLVM,
                    NLVM = x.NLVM,
                    Documento = x.Documento,
                    Obra = x.Obra.Obra,
                    NumeroLVM = x.NLVM,
                    LVM = x.MascaraLVM,
                    Data = x.Data,
                    NotaFiscal = x.NotaFiscal,
                    OC = x.OC,
                    Invoice = x.RM,
                    QtdItem = x.RC,
                    Material = x.Material != null ? x.Material.Material : string.Empty,
                    Disciplina = x.Disciplina != null ? x.Disciplina.Disciplina : string.Empty,
                    TipoOC = x.TipoOC != null ? x.TipoOC.Nome : string.Empty,
                    RM = x.RM,
                    RC = x.RC,
                    IdFornecedor = x.IdFornecedor,
                    IdMaterial = x.IdMaterial,
                    IdDisciplina = x.IdDisciplina,
                    IdTipoOC = x.IdTipoOC,
                    Valor = x.Valor,
                    Obs = x.Obs
                });
        }


    }
}
