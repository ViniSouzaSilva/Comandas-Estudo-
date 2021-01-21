namespace AmbiStore.Shared.EFCore.Models
{
    public class SERVICO : EntityBase
    {
        public string COD_LST { get; set; }
        public decimal? ISS_ALIQ { get; set; }
        public string CODIGO_IBPT { get; set; }
        public string CNAE { get; set; }
        public string CST { get; set; }


        public int ESTOQUE_ID { get; set; }
        public ESTOQUE ESTOQUE { get; set; }

    }
}