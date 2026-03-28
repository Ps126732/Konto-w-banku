using Bank; 
using System;

namespace Bank.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SYMULACJA SYSTEMU BANKOWEGO (KROK 4) ===\n");

            
            Console.WriteLine("--- 1. Zwykłe Konto ---");
            Konto molenda = new Konto("Molenda", 100);
            Console.WriteLine($"Utworzono konto dla: {molenda.Nazwa}, Bilans: {molenda.Bilans} PLN");

            molenda.Wplata(50);
            Console.WriteLine($"Po wpłacie 50 PLN, bilans: {molenda.Bilans} PLN");

            Console.WriteLine("Próba wypłaty 200 PLN (więcej niż na koncie):");
            try
            {
                molenda.Wyplata(200);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD]: {ex.Message}");
            }


            
            Console.WriteLine("\n--- 2. KontoPlus (Z limitem debetowym 100 PLN) ---");
            KontoPlus kowalski = new KontoPlus("Kowalski", 50, 100);
            Console.WriteLine($"Utworzono KontoPlus. Do dyspozycji (środki + limit): {kowalski.Bilans} PLN");

            Console.WriteLine("Wypłacamy 100 PLN (50 własnych + 50 z debetu)...");
            kowalski.Wyplata(100);

            Console.WriteLine($"Po wypłacie. Do dyspozycji: {kowalski.Bilans} PLN");
            Console.WriteLine($"Czy konto zostało zablokowane po wejściu w debet? {kowalski.Zablokowane}");


            Console.WriteLine("\n--- 3. KontoLimit (Delegacja, limit 200 PLN) ---");
            KontoLimit nowak = new KontoLimit("Nowak", 0, 200);
            Console.WriteLine($"Utworzono KontoLimit. Do dyspozycji: {nowak.Bilans} PLN");

            Console.WriteLine("Wypłacamy 150 PLN wchodząc w debet...");
            nowak.Wyplata(150);
            Console.WriteLine($"Czy konto zablokowane? {nowak.Zablokowane}");

            Console.WriteLine("Spłacamy debet wpłacając 200 PLN...");
            nowak.Wplata(200);
            Console.WriteLine($"Po spłacie. Czy zablokowane? {nowak.Zablokowane}");
            Console.WriteLine($"Nowy bilans do dyspozycji: {nowak.Bilans} PLN");

            Console.ReadLine(); 
        }
    }
}