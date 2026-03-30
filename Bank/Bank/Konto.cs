using System;

namespace Bank
{
    public class Konto
    {
        private string klient;
        private decimal bilans;
        private bool zablokowane = false;

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            if (string.IsNullOrWhiteSpace(klient))
                throw new ArgumentException("Nazwa klienta nie może być pusta.");

            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa => klient;
        public virtual decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public virtual void Wplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException("Konto zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Kwota musi być dodatnia.");
            bilans += kwota;
        }

        public virtual void Wyplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException("Konto zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Kwota musi być dodatnia.");
            if (kwota > bilans) throw new InvalidOperationException("Brak środków.");
            bilans -= kwota;
        }

        public void BlokujKonto() => zablokowane = true;
        public void OdblokujKonto() => zablokowane = false;
    }
}