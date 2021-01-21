using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.Extension
{
    public static class DecimalExtensions
    {
        public static decimal RoundABNT(this decimal value, int places = 2)
        {

            decimal a, b, c, d;
            decimal algAManter, algAAvaliar, algsADireita;

            if (places != 3)
            {
                a = ExtraiDigitoSignificativo(value);
                b = ExtraiDigitoSignificativo(a);
                c = ExtraiDigitoSignificativo(b);

                algAAvaliar = Math.Truncate(c);
                algsADireita = c - Math.Truncate(c);
                algAManter = Math.Truncate(b);
                places = 2;
            }
            else
            {
                a = ExtraiDigitoSignificativo(value);
                b = ExtraiDigitoSignificativo(a);
                c = ExtraiDigitoSignificativo(b);
                d = ExtraiDigitoSignificativo(c);

                algAAvaliar = Math.Truncate(d);
                algsADireita = d - Math.Truncate(d);
                algAManter = Math.Truncate(c);
            }

            if (algAAvaliar < 5)
            {
                return Math.Truncate(value * (decimal)Math.Pow(10, places)) / (decimal)Math.Pow(10, places);
            }
            if (algAAvaliar > 5)
            {
                return (Math.Truncate((value * (decimal)Math.Pow(10, places)) + 1) / (decimal)Math.Pow(10, places));
            }
            if (algAAvaliar == 5)
            {
                if (algsADireita != 0)
                {
                    return (Math.Truncate((value * (decimal)Math.Pow(10, places)) + 1) / (decimal)Math.Pow(10, places));
                }
                if (algsADireita == 0)
                {
                    if (algAManter % 2 == 1)
                    {
                        return (Math.Truncate((value * (decimal)Math.Pow(10, places)) + 1) / (decimal)Math.Pow(10, places));
                    }
                    if (algAManter % 2 == 0)
                    {
                        return Math.Truncate(value * (decimal)Math.Pow(10, places)) / (decimal)Math.Pow(10, places);
                    }
                }
            }
            return value;
        }
        private static decimal ExtraiDigitoSignificativo(decimal valor)
        {
            return (valor - Math.Truncate(valor)) * 10;
        }

    }
}
