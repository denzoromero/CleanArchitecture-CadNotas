using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.BSCore.StateCity
{
    [Authorize]
    public record StateQuery : IRequest<List<DropdownListVM>?>;

    public class StateCityQueryHandler(IContextBS contextBS) : IRequestHandler<StateQuery, List<DropdownListVM>?>
    {
        private readonly IContextBS _contextBS = contextBS;

        public async Task<List<DropdownListVM>?> Handle(StateQuery req, CancellationToken cancellationToken)
        {
            var States = await _contextBS.Estados.Where(i => i.Ativo == 1)
                        .Select(i => new DropdownListVM
                        {
                            Id = i.Id,
                            Name = i.Nome
                        }).ToListAsync(cancellationToken);

            return States;
        }

    }


}
