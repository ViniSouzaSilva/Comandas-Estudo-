using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace AmbiStore.Shared.EFCore.Models
{
    public class GRUPO : EntityBase
    {
        [Required]
        public string NOME { get; set; } = string.Empty;
        public List<ESTOQUE> ESTOQUE_GRUPO { get; set; } //Lista de Estoques que pertencem a esse grupo
    }
}