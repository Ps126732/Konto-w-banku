namespace Bank
{
    public class DekoratorKontoPlus : IKonto
    {
        private IKonto kontoBaza; 
        private decimal limit;

        public DekoratorKontoPlus(IKonto kontoDoZmiany, decimal limit)
        {
            this.kontoBaza = kontoDoZmiany;
            this.limit = limit;
        }

        public string Nazwa => kontoBaza.Nazwa;
        public decimal Bilans => kontoBaza.Bilans + limit;
        public bool Zablokowane => kontoBaza.Zablokowane;

        public void Wplata(decimal kwota) => kontoBaza.Wplata(kwota);
        public void Wyplata(decimal kwota) => kontoBaza.Wyplata(kwota);
        public void BlokujKonto() => kontoBaza.BlokujKonto();
        public void OdblokujKonto() => kontoBaza.OdblokujKonto();

    
        public IKonto ZrezygnujZLimitu()
        {
            return kontoBaza;
        }
    }
}