using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Objetos
{
    class EmpresasCadastradas
    {
        public class Nfe
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public string motivo { get; set; }
            public DateTime dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Nfce
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Mdfe
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Cte
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Nfse
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Cfesat
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Gnre
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }

        public class Cteos
        {
            public int situacao { get; set; }
            public int tipocontrato { get; set; }
            public object motivo { get; set; }
            public object dataativacao { get; set; }
            public object datainativacao { get; set; }
        }
        public class Dado
        {
            public int handle { get; set; }
            public int idgrupo { get; set; }
            public int iduf { get; set; }
            public int idcidade { get; set; }
            public string cnpj { get; set; }
            public string identificacao { get; set; }
            public string descricao { get; set; }
            public string certificado { get; set; }
            public string inscricaomunicipal { get; set; }
            public string    inscricaoestadual { get; set; }
            public string tipocertificado { get; set; }
            public string    pincode { get; set; }
            public string razaosocial { get; set; }
            public string endereco { get; set; }
            public string    telefone { get; set; }
            public string email { get; set; }
            public string criadoem { get; set; }
            public string atualizadoem { get; set; }
            public Nfe nfe { get; set; }
            public Nfce nfce { get; set; }
            public Mdfe mdfe { get; set; }
            public Cte cte { get; set; }
            public Nfse nfse { get; set; }
            public Cfesat cfesat { get; set; }
            public Gnre gnre { get; set; }
            public Cteos cteos { get; set; }
            public int situacao { get; set; }
            public string datainativacao { get; set; }
            public string motivo { get; set; }
            public string hashcertificado { get; set; }
            public string dtvencimentocertificado { get; set; }
            public string senhacertificado { get; set; }
        }

        public class ConsultarEmpresasCadastradas
        {
            public string mensagem { get; set; }
            public List<Dado> dados { get; set; }
            public int total { get; set; }
        }




    }
}
