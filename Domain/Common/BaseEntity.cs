using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime? DataRegistro { get; protected set; }
        public int Ativo { get; protected set; }
    }
}
