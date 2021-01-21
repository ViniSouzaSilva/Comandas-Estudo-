using System.ComponentModel.DataAnnotations.Schema;

namespace AmbiStore.Shared.EFCore.Models
{
    public class PED_COMPRA_ITEM : EntityBase
    {
        [ForeignKey("ID")]
        public PEDIDO_COMPRA ID_PED_COMPRA { get; set; }
        public ESTOQUE ITEM { get; set; }
        public decimal QUANTIDADE { get; set; }
    }
}