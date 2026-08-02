using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos
{
    public record ProjetosVM: CommonVM
    {
        public string Projeto { get; init; } = string.Empty;
        public string Obras { get; init; } = string.Empty;
        public IEnumerable<int> IdObras { get; init; } = [];
    }
}
