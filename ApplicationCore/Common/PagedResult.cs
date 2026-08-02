using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common
{
    public record PagedResult<T>
    {
        public required IReadOnlyCollection<T> Items { get; init; }
        public required int TotalCount { get; init; }
        public required int Page { get; init; }
        public int PageSize { get; init; } = Constants.ITEMS_PER_PAGE;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
