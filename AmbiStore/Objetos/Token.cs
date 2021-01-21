using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Objetos
{
    public class Token
    {
        public string mensagem { get; set; }
        public Dados dados { get; set; }
    }
    public class Dados 
    {
        public string token { get; set; }
        public DateTime tokenexpiracao { get; set; }
    }
}
