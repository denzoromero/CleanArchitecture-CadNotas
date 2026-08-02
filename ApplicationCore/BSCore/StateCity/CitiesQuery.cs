using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.BSCore.StateCity
{
    [Authorize]
    public record CitiesQuery : IRequest<List<DropdownListVM>?>
    {
        public int? Id { get; init; }
    }

    public class CitiesQueryHandler(IContextBS contextBS) : IRequestHandler<CitiesQuery, List<DropdownListVM>?>
    {
        private readonly IContextBS _contextBS = contextBS;

        public async Task<List<DropdownListVM>?> Handle(CitiesQuery req, CancellationToken cancellationToken)
        {
            var Cities = await _contextBS.Cidades.Where(i => i.Ativo == 1 && i.IdEstado == req.Id.ToString())
                        .Select(i => new DropdownListVM
                        {
                            Id = i.Id,
                            Name = i.Nome
                        }).ToListAsync(cancellationToken);

            return Cities;
        }
    }


}
