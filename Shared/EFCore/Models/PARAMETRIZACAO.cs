using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class PARAMETRIZACAO : EntityBase
    {
        public int FORNECEDOR_ID { get; set; }
        public CONTATO FORNECEDOR { get; set; }
        public string COD_FORNECEDOR { get; set; }
        public int ESTOQUE_ID { get; set; }
        public ESTOQUE ESTOQUE { get; set; }

    }
}
