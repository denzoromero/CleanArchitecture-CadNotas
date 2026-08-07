using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum ClvmStatus
    {
        [Display(Name = "Selecionar...")]
        None = 0,
        [Display(Name = "Aprovado")]
        Aprovado = 1,
        [Display(Name = "Reprovado")]
        Reprovado = 2,
        [Display(Name = "Em Andamento")]
        EmAndamento = 3,
        [Display(Name = "Parc. Aprovado")]
        ParcialmenteAprovado = 4
    }


}
