using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Commands
{
    public abstract record MaterialDTO
    {
        public string Material { get; init; } = string.Empty;
    }
}
