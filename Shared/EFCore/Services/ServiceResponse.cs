using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Services
{
    public class ServiceResponse
    {
        public ServiceResponseStatus Srs { get; set; }
        public string Resposta { get; set; }
        public string Erro { get; set; }
        public Exception exception { get; set; }

        public ServiceResponse()
        {

        }

        public ServiceResponse(ServiceResponseStatus srs, string resposta)
        {
            Srs = srs;
            Resposta = resposta;
        }

        public ServiceResponse(ServiceResponseStatus srs, string resposta, string erro, Exception ex = null) : this(srs, resposta)
        {
            Erro = erro;
            exception = ex;
        }

        public enum ServiceResponseStatus
        {
            Interrompido,
            Concluído,
            ComPendências
        }
    }
}
