using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.Exceptions
{
    public class NullInfoException : ApplicationException
    {
        public NullInfoException(string campo) : base($"Campo obrigatório estava em branco: {campo}")
        {
        
        }
    }
}
