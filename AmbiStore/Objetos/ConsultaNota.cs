using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Objetos
{
   public  class ConsultaNota
  
    {
        public string Handle { get; set; }
        public string Chave  { get; set; }
        public string Situacao { get; set; }
        public string Nnf { get; set; }
        public string Codnf { get; set; }
        public string Nrecibo { get; set; }
        public string Nprotenvio { get; set; }
        public string Nprotcanc { get; set; }
        public string Nprotinutil{ get; set; }
        public string Nregdpec { get; set; }
        public string Modoentrada { get; set; }
        public string Modosaida { get; set; }
        public string Cnpjemissor { get; set; }
        public string Serie { get; set; }
        public string Motivo { get; set; }
        public string Dtautorizacao { get; set; }
        public string Dtcadastro { get; set; }
        public string Dtcancelamento { get; set; }
        public string Dtemissao { get; set; }
        public string Impresso { get; set; }
        public string Envemail { get; set; }
        public string Email { get; set; }
        public string Docdestinatario { get; set; }
        public string Nomedestinatario { get; set; }
        public string Id_grupo { get; set; }
        public string Id_integracao { get; set; }
        public string Nlote { get; set; }
        public string Numero { get; set; }
        public string Dhdpec { get; set; }
        public string Nomegrupo { get; set; }
        public string Xjustcancelamento { get; set; }
        public string Ambiente { get; set; }
        public string Impressora { get; set; }
        public string Origem { get; set; }
        public string Sincronizadopm  { get; set; }
        public string Cstat { get; set; }
        public string Importado { get; set; }
        public string Valortotal { get; set; }
        public string Destinada { get; set; }
        public string Xmldestinatario { get; set; }

    }
    public class inutilizacao 
    {
        public string Cnpj { get; set; }
        public string Serie { get; set; }
        public string Nfini { get; set; }
        public string Nffin { get; set; }
        public string Nrprotocolo { get; set; }
        public string Ano { get; set; }
        public string Justificativa { get; set; }
        public string Ambiente { get; set; }
         
    }
}
