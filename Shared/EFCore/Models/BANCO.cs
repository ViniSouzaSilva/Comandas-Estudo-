using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class BANCO
    {
        public List<BANCO_CONTA> BANCO_CONTAS { get; set; }
        [Required]
        [Key]
        public int NUM_BANCO { get; set; }
        [Required]
        public string NOME_BANCO { get; set; }
    }
}
