using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbiStore.Shared.EFCore.Models
{
    public class DAV_ITEM : EntityBase
    {
        [ForeignKey("ID")]
        public DAV ID_DAV { get; set; }
        public ESTOQUE ESTOQUE { get; set; }
        public decimal QTD_ITEM { get; set; }
        public decimal VLR_UNIT { get; set; }
        public decimal VLR_DESC_TOTAL { get; set; }
    }
}