using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CARGO_FUNCIONARIO : EntityBase
    {
        [ForeignKey("FUNCIONARIO")]
        public int FUNCIONARIOID { get; set; }
        public FUNCIONARIO FUNCIONARIO { get; set; }
        [Required]
        public Cargo CARGO { get; set; }

    }
}
