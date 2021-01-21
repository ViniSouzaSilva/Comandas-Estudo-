namespace AmbiStore.Shared.EFCore.Models
{
    public class COMPRA_PAGAMENTO : EntityBase
    {
        public decimal VLR_PAGTO { get; set; }
        public COMPRA COMPRA { get; set; }
        public FORMAPAGAMENTO FORMAPAGAMENTO { get; set; }
    }
}