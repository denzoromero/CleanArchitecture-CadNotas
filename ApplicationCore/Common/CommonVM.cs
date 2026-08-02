using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common
{
    public abstract record CommonVM
    {
        public int? Id { get; init; }
        public DateTime? DataRegistro { get; init; }
        public int? Ativo { get; init; }
    }

    public record DropdownListVM
    {
        public int? Id { get; init; }
        public string? Name { get; init; } = string.Empty;
    }


}
