using System;
using Bank;

namespace Bank.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Krok 4: Symulacja operacji
            Konto molenda = new Konto("Molenda", 100m);
            molenda.Wplata(50m);
            Console.WriteLine($"[Konto] {molenda.Nazwa} | Bilans: {molenda.Bilans}");

            KontoPlus kontoPlus = new KontoPlus("Nowak", 100m, 500m);
            kontoPlus.Wyplata(300m);
            Console.WriteLine($"[KontoPlus] {kontoPlus.Nazwa} | Bilans: {kontoPlus.Bilans} | Zablokowane: {kontoPlus.Zablokowane}");

            KontoLimit kontoLimit = new KontoLimit("Kowalski", 200m, 300m);
            kontoLimit.Wyplata(400m);
            kontoLimit.Wplata(400m);
            Console.WriteLine($"[KontoLimit] {kontoLimit.Nazwa} | Bilans: {kontoLimit.Bilans} | Zablokowane: {kontoLimit.Zablokowane}");

            IKonto klient = new Konto("Jan Kowalski", 100m);
            Console.WriteLine($"Krok 1: Zwykłe konto. Bilans: {klient.Bilans} PLN");

            klient = new DekoratorKontoPlus(klient, 500m);
            Console.WriteLine($"Krok 2: Dodano limit. Nowy bilans do dyspozycji: {klient.Bilans} PLN");

            klient = ((DekoratorKontoPlus)klient).ZrezygnujZLimitu();
            Console.WriteLine($"Krok 3: Rezygnacja z limitu. Bilans wraca do normy: {klient.Bilans} PLN");


            Console.ReadKey();
        }
    }
}