using ApplicationCore.Common.Validators.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries
{
    public class SearchNotaFiscaisValidator : CommonSearchQuery<SearchNotaFiscais>
    {
        public SearchNotaFiscaisValidator()
        {

        }

    }
}
