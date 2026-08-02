using ApplicationCore.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IAuditLogger
    {
        Task LogAsync(AuditEntry entry);
    }
}
