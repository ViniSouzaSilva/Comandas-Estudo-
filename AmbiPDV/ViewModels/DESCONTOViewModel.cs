using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AmbiPDV.ViewModels
{
    public class DESCONTOViewModel : ViewModelBase
    {
        private decimal pORCENTAGEM;
        private decimal dESCONTO_ABSOLUTO;

        public decimal PORCENTAGEM { get => pORCENTAGEM; set { pORCENTAGEM = value; if (dESCONTO_ABSOLUTO != 0) dESCONTO_ABSOLUTO = 0; OnPropertyChanged("DESCONTO_ABSOLUTO"); } }
        public decimal DESCONTO_ABSOLUTO { get => dESCONTO_ABSOLUTO; set { dESCONTO_ABSOLUTO = value; if (pORCENTAGEM != 0) pORCENTAGEM = 0; OnPropertyChanged("PORCENTAGEM"); } }

        internal bool ValidaDesconto()
        {
            if ((PORCENTAGEM == 0) && (DESCONTO_ABSOLUTO == 0))
            {
                MessageBox.Show("Desconto não pode ser zero");
                return false;
            }
            if (PORCENTAGEM > 1 || PORCENTAGEM < 0 || DESCONTO_ABSOLUTO < 0)
            {
                MessageBox.Show("Valor de desconto inválido. Não pode ser negativo, nem maior que 100%");
                return false;
            }
            else return true;
        }
        
        public (decimal, TipoDesconto) DescontoAAplicar()
        {
            if (PORCENTAGEM > 0) return (PORCENTAGEM, TipoDesconto.Porcentual);
            if (DESCONTO_ABSOLUTO > 0) return (DESCONTO_ABSOLUTO, TipoDesconto.Absoluto);
            else throw new FlowException("Tipo de desconto inválido. Deve ser Porcentual ou Absoluto");
        }
    }
}
