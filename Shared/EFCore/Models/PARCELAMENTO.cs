using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class PARCELAMENTO : EntityBase
    { 
        [MaxLength(40)]
        public string DESCRICAO { get; set; }
        public int NUMERO_PARCELA { get; set; }// 0(zero) QUER DIZER A VISTA
        [NotMapped]
        public List<int> INTERVALOS {
            get
            {
                if (String.IsNullOrWhiteSpace(INTERVALO)) return null;
                List<int> retorno = new List<int>();
                foreach (string item in INTERVALO.Split('|'))
                {
                    retorno.Add(int.Parse(item));
                }
                return retorno;
            }
            set
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < value.Count; i++)
                {
                    sb.Append($"{value[i]}|");
                }
                INTERVALO = sb.ToString().TrimEnd('|');
            }
        }
        public string INTERVALO { get; set; }
        public bool ENTRADA { get; set; }
        public Status STATUS { get; set; }
        //===================
        public List<FORMAPAGAMENTO> PARCELAMENTO_FORMAPAGAMENTO { get; set; }
    }
}
