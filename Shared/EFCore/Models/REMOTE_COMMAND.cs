using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class REMOTE_COMMAND : EntityBase
    {
        [Required]
        public TipoComando TIPO_COMANDO { get; set; }
        [Required]
        public string INFORMACAO { get; set; }
    }
}
