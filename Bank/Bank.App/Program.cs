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

            Console.ReadKey();
        }
    }
}