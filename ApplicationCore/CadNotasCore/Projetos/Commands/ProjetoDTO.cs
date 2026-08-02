using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Commands
{
    public abstract record ProjetoDTO
    {
        public string Projeto { get; init; } = string.Empty;
        public List<int> IdObras { get; init; } = [];
    }
}
