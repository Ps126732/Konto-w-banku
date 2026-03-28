using Bank;
Konto molenda = new Konto("Molenda", 100);
Console.WriteLine($"Konto: założone. Klient: {molenda.Nazwa}, Bilans: {molenda.Bilans}");

molenda.Wplata(50);
Console.WriteLine($"Po wpłacie bilans wynosi: {molenda.Bilans}");
