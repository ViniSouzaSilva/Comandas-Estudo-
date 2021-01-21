using System.ComponentModel.DataAnnotations.Schema;

namespace AmbiStore.Shared.EFCore.Models
{
    public class REMESSA_ITEM : EntityBase
    {
        [ForeignKey("ID")]
        public REMESSA ID_REMESSA { get; set; }
        public ESTOQUE ITEM { get; set; }
        public decimal QUANTIDADE { get; set; }
    }
}