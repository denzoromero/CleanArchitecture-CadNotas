using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Common.DTO
{
    public abstract record class SearchDTO
    {
        public string? Filter { get; init; }
        public bool? Ativo { get; init; }
        public int PageNo { get; init; }

        public int AtivoValue => Ativo == true ? 1 : 0;
    }




}
