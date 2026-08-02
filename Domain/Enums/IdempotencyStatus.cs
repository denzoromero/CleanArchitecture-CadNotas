using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum IdempotencyStatus
    {
        Processing = 1,
        Completed = 2,
        Failed = 3
    }
}
