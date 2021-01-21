using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.Exceptions
{
    public class InvalidValueException : ApplicationException
    {
        object ErrorGenerator;
        public InvalidValueException(string campo, object errorObject = null) : base($"Valor inválido para o campo: {campo}")
        {
            ErrorGenerator = errorObject;
        }
    }
}
