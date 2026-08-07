using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries
{
    public record GetDropDownLists : IRequest<NotaFiscaisDDL>;

    public class GetDropDownListsQueryHandler(IContextCad context) : IRequestHandler<GetDropDownLists, NotaFiscaisDDL>
    {
        private readonly IContextCad _contextCad = context;
        public async Task<NotaFiscaisDDL> Handle(GetDropDownLists query, CancellationToken cancellationToken)
        {
            var obras = await _contextCad.CadObras.Where(i => i.Ativo == 1)
                        .Select(i => new DropdownListVM
                        {
                            Id = i.Id,
                            Name = i.Obra
                        }).ToListAsync(cancellationToken);

            var fornecedors = await _contextCad.CadFornecedors.Where(i => i.Ativo == 1)
                   .Select(i => new DropdownListVM
                   {
                       Id = i.Id,
                       Name = i.Fornecedor
                   }).ToListAsync(cancellationToken);

            var materials = await _contextCad.CadMaterials.Where(i => i.Ativo == 1)
                  .Select(i => new DropdownListVM
                  {
                      Id = i.Id,
                      Name = i.Material
                  }).ToListAsync(cancellationToken);

            var disciplinas = await _contextCad.CadDisciplinas.Where(i => i.Ativo == 1)
                 .Select(i => new DropdownListVM
                 {
                     Id = i.Id,
                     Name = i.Disciplina
                 }).ToListAsync(cancellationToken);

            var tipoocs = await _contextCad.CadTipoOCs.Where(i => i.Ativo == 1)
                .Select(i => new DropdownListVM
                {
                    Id = i.Id,
                    Name = i.Nome
                }).ToListAsync(cancellationToken);


            return new NotaFiscaisDDL(obras,fornecedors,materials,disciplinas,tipoocs);
        }
    }


}
