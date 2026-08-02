using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Queries
{
    [Authorize]
    public record GetObraList : IRequest<List<DropdownListVM>>;

    public class GetObraListQueryHandler(IContextCad contextCad) : IRequestHandler<GetObraList, List<DropdownListVM>>
    {
        private readonly IContextCad _contextCad = contextCad;
        public async Task<List<DropdownListVM>> Handle(GetObraList query, CancellationToken cancellationToken)
        {
            var obras = await _contextCad.CadObras.Where(i => i.Ativo == 1)
                        .Select(i => new DropdownListVM
                        {
                            Id = i.Id,
                            Name = i.Obra
                        }).ToListAsync(cancellationToken);

            return obras;

        }
    }


}
