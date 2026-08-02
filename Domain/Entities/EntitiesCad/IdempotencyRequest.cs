using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class IdempotencyRequest
    {
        public long Id { get; set; }
        public Guid IdempotencyKey { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public int? UserId { get; set; }
        public IdempotencyStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }


}
