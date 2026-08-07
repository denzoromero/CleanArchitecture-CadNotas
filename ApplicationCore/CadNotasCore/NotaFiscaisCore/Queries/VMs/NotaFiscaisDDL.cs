using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs
{
    public record NotaFiscaisDDL(
        IEnumerable<DropdownListVM> Obras,
        IEnumerable<DropdownListVM> Fornecedors,
        IEnumerable<DropdownListVM> Materials,
        IEnumerable<DropdownListVM> Disciplinas,
        IEnumerable<DropdownListVM> TipoOCs);
}
