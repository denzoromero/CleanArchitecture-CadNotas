using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Queries
{
    public record MaterialVM : CommonVM
    {
        public string Material { get; init; } = string.Empty;
    }
}
