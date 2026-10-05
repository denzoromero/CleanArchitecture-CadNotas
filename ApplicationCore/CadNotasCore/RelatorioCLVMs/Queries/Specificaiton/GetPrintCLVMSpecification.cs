using ApplicationCore.CadNotasCore.RelatorioCLVMs.Queries.VM;
using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.RelatorioCLVMs.Queries.Specificaiton
{
    public class GetPrintCLVMSpecification : Specification<EntityLVM, GetPrintCLVMPageVM>
    {
        public GetPrintCLVMSpecification(int IdLVM) 
        {
            Query.AsNoTracking().Where(i => i.Id == IdLVM)
                .Select(i => new GetPrintCLVMPageVM
                {       
                    IdLVM = i.Id,
                    Id = i.RelatorioCLVM != null ? i.RelatorioCLVM.Id : null,
                    IdLVMs = i.RelatorioCLVM != null ? i.RelatorioCLVM.IdLVM : string.Empty,
                    Observacao = i.RelatorioCLVM != null ? i.RelatorioCLVM.Observacao : string.Empty,
                    IdInspetor = i.RelatorioCLVM != null ? i.RelatorioCLVM.IdInspetor : null,
                    DtInspetor = i.RelatorioCLVM != null ? i.RelatorioCLVM.DtInspetor : null,
                    IdVerificador = i.RelatorioCLVM != null ? i.RelatorioCLVM.IdVerificador : null,
                    DtVerificar = i.RelatorioCLVM != null ? i.RelatorioCLVM.DtVerificar : null,
                });
        }
    }
}
