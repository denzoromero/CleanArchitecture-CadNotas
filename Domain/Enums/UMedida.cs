using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum UMedida
    {
        [Display(Name = "Selecionar...")]
        None = 0,
        PC = 1,
        LG = 2,
        Unit = 3,
        M = 4,
        [Display(Name = "M²")]
        M2 = 5,
        Mm = 6,
        T = 7,
        Kg = 8,
        Lt = 9
    }

}
