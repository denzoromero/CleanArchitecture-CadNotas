using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Disciplinas
{
    public record DisciplinaVM : CommonVM
    {
        public string Disciplina { get; init; } = string.Empty;
    }
}
