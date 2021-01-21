using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Objetos
{
    class GruposCadastrados
    {
        
        public class Dado
        {
            
            public string identificacao { get; set; }
            public string descricao { get; set; }
            public int idusuario { get; set; }
            public int handle { get; set; }
            public int idsoftwarehouse { get; set; }
            public DateTime criadoem { get; set; }
            public DateTime atualizadoem { get; set; }
        }

        public class Grupos
        {
            public string mensagem { get; set; }
            public List<Dado> dados { get; set; }
            public int total { get; set; }
        }




    }
}
