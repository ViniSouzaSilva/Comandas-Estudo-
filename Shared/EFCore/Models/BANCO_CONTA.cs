using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbiStore.Shared.EFCore.Models
{
    public class BANCO_CONTA : EntityBase
    {
        [Required]
        [ForeignKey("ID")]
        public BANCO BANCO { get; set; }
        [Required]
        public int AGENCIA { get; set; }
        [Required]
        public string CONTA { get; set; }
        public string DESCRICAO { get; set; }
    }
}