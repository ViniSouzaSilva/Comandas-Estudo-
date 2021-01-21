using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.Auxiliares
{

    public class APICambio
    {
        public XChangeInfo USD { get; set; } //Dolar Comercial
        public XChangeInfo USDT { get; set; } //Dolar Turismo
        public XChangeInfo CAD { get; set; } // Dolar Canandense
        public XChangeInfo EUR { get; set; } //Euro
        public XChangeInfo GBP { get; set; } //Libra Britânica
        public XChangeInfo ARS { get; set; } //Peso Argentino
        public XChangeInfo BTC { get; set; } //Bitcoin
        public XChangeInfo LTC { get; set; } //Litecoin
        public XChangeInfo JPY { get; set; } //Iene Japones
        public XChangeInfo CHF { get; set; } //Franco Suíço
        public XChangeInfo AUD { get; set; } //Dolar Australiano
        public XChangeInfo CNY { get; set; } // Iuan Chinês
        public XChangeInfo ILS { get; set; } // Novo Shekel Israelense
        public XChangeInfo ETH { get; set; } // Etherium
        public XChangeInfo XRP { get; set; } // Ripple
    }

    public class XChangeInfo
    {
        public string code { get; set; }
        public string codein { get; set; }
        public string name { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string varBid { get; set; }
        public string pctChange { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string timestamp { get; set; }
        public string create_date { get; set; }
    }
}
