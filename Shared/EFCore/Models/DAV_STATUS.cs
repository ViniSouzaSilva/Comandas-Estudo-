using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class DAV_STATUS : EntityBase
    {
        public string DESCRICAO { get; set; }
        public bool RESERVA { get; set; }

        public DAV_STATUS()
        {

        }

        public DAV_STATUS(int iD, string dESCRICAO, bool rESERVA)
        {
            ID = iD;
            DESCRICAO = dESCRICAO;
            RESERVA = rESERVA;
        }
    }
}
