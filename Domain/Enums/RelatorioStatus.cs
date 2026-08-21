using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum RelatorioStatus
    {
        [Display(Name = "Selecionar...")]
        None = 0,
        [Display(Name = "Aprovado")]
        Aprovado = 1,
        [Display(Name = "Nao Conforme")]
        NaoConforme = 2,
        [Display(Name = "Conf. Observação")]
        ConfObservacao = 3,
    }
}
