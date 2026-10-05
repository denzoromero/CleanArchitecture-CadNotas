using Ardalis.Specification;
using DocumentFormat.OpenXml.Office2021.Excel.NamedSheetViews;
using Domain;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.RDFAs.Queries.GetLVMRDFA
{
    public class GetLVMRDFASpecificaiton : Specification<EntityLVM>
    {
        public static Expression<Func<EntityLVM, bool>> BuildFilter(string? mascara, int idObra)
        {
            return i => (string.IsNullOrEmpty(mascara) || i.MascaraLVM == mascara)
                                && i.IdObra == idObra && i.Ativo == 1;
        }

        public GetLVMRDFASpecificaiton(string? mascara, int idObra, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(mascara, idObra))
                     .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE);

        }
    }
}
