using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static AmbiStore.Shared.Extension.StringExtensions;

namespace AmbiStore.Shared.Libraries.Validations
{
    public class CPForCPNPJ : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contato = (CONTATO)validationContext.ObjectInstance;
            if (!(contato.CONTATO_PF is null))
            {
                if (!contato.CONTATO_PF.CPF.TiraPont().IsCpf())
                {
                    return new ValidationResult("Insira um CPF válido!");
                }
                else return ValidationResult.Success;
            }
            if (!(contato.CONTATO_PJ is null))
            {
                if (!contato.CONTATO_PJ.CNPJ.TiraPont().IsCnpj())
                {
                    return new ValidationResult("Insira um CNPJ válido!");
                }
                else return ValidationResult.Success;
            }
            return base.IsValid(value, validationContext);
        }
    }

}
