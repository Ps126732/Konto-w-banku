using System;
using static System.Net.Mime.MediaTypeNames;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal limit;
        private decimal wykorzystanyDebet = 0; 

  
        public decimal Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public override decimal Bilans
        {
            get
            {
         
                if (Zablokowane) return 0;

                return base.Bilans + limit;
            }
        }

        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 100)
            : base(klient, bilansNaStart)
        {
            this.limit = limit;
        }

        public override void Wplata(decimal kwota)
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
                        base.Wplata(reszta);
                    }
                }
                else
                {

                    wykorzystanyDebet -= kwota;
                }
            }
            else
            {

                base.Wplata(kwota);
            }
        }

        public override void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane. Dokonaj wpłaty, aby spłacić debet.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");

            if (kwota <= base.Bilans)
            {
             
                base.Wyplata(kwota);
            }
            else
            {
          
                decimal brakujacaKwota = kwota - base.Bilans;

                if (brakujacaKwota <= limit)
                {
                   
                    if (base.Bilans > 0)
                    {
                        base.Wyplata(base.Bilans);
                    }

                    wykorzystanyDebet = brakujacaKwota;

       
                    BlokujKonto();
                }
                else
                {
                    throw new InvalidOperationException("Kwota przekracza dostępny jednorazowy limit debetowy.");
                }
            }
        }
    }
}