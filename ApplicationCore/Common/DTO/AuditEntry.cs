using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.DTO
{
    public record AuditEntry(string EntityName, string Action);

}
