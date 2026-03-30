using System;
using static System.Net.Mime.MediaTypeNames;

namespace Bank
{
    public class KontoLimit
    {
      
        private Konto konto;

        private decimal limit;
        private decimal wykorzystanyDebet = 0;

        public decimal Limit
        {
            get { return limit; }
            set { limit = value; }
        }

      
        public string Nazwa => konto.Nazwa;
        public bool Zablokowane => konto.Zablokowane;

      
        public decimal Bilans
        {
            get
            {
                if (Zablokowane) return 0;
                return konto.Bilans + limit;
            }
        }

       
        public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 100)
        {
            this.konto = new Konto(klient, bilansNaStart);
            this.limit = limit;
        }

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota wpłaty musi być dodatnia.");

            if (Zablokowane && wykorzystanyDebet > 0)
            {
                if (kwota >= wykorzystanyDebet)
                {
                    decimal reszta = kwota - wykorzystanyDebet;
                    wykorzystanyDebet = 0;
                    OdblokujKonto(); 

                    if (reszta > 0)
                    {
                        konto.Wplata(reszta); 
                    }
                }
                else
                {
                    wykorzystanyDebet -= kwota;
                }
            }
            else
            {
                konto.Wplata(kwota); 
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");

            if (kwota <= konto.Bilans)
            {
               
                konto.Wyplata(kwota);
            }
            else
            {
     
                decimal brakujacaKwota = kwota - konto.Bilans;

                if (brakujacaKwota <= limit)
                {
                  
                    if (konto.Bilans > 0)
                    {
                        konto.Wyplata(konto.Bilans);
                    }

                    wykorzystanyDebet = brakujacaKwota;
                    BlokujKonto(); 
                }
                else
                {
                    throw new InvalidOperationException("Przekroczono dostępny jednorazowy limit debetowy.");
                }
            }
        }

      
        public void BlokujKonto() => konto.BlokujKonto();
        public void OdblokujKonto() => konto.OdblokujKonto();
    }
}