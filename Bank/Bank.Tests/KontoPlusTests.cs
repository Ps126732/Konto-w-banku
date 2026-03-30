using System;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Bank;

namespace Bank.Tests
{
    [TestClass] 
    public class KontoPlusTests
    {
        [TestMethod] 
        public void Bilans_ZwracaSrodkiDoDyspozycji()
        {
            
            KontoPlus konto = new KontoPlus("Jan Kowalski", 100, 200);

           
            Assert.AreEqual(300, konto.Bilans);
        }

        [TestMethod] 
        public void Wyplata_ZWykorzystaniemDebetu_BlokujeKonto()
        {
            
            KontoPlus konto = new KontoPlus("Jan Kowalski", 100, 200);

           
            konto.Wyplata(250); 

            
            Assert.IsTrue(konto.Zablokowane, "Konto powinno być zablokowane po jednorazowym debecie.");
            Assert.AreEqual(0, konto.Bilans, "Po zablokowaniu bilans do dyspozycji powinien wynosić 0.");
        }

        [TestMethod] 
        public void Wyplata_PrzekroczenieLimitu_RzucaWyjatek()
        {
            
            KontoPlus konto = new KontoPlus("Jan Kowalski", 100, 50);

            
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [TestMethod] 
        public void Wplata_CzesciowaSplataDebetu_NieOdblokowujeKonta()
        {
            
            KontoPlus konto = new KontoPlus("Jan Kowalski", 0, 100);
            konto.Wyplata(100); 

            
            konto.Wplata(50); 

            
            Assert.IsTrue(konto.Zablokowane, "Konto powinno pozostać zablokowane przy niepełnej spłacie.");
        }

        [TestMethod] 
        public void Wplata_CalkowitaSplataDebetu_OdblokowujeKontoOrazPrzywracaLimit()
        {
            
            KontoPlus konto = new KontoPlus("Jan Kowalski", 50, 100);
            konto.Wyplata(100); 

           
            konto.Wplata(60); 

           
            Assert.IsFalse(konto.Zablokowane, "Konto powinno się odblokować po całkowitej spłacie.");
          
            Assert.AreEqual(110, konto.Bilans);
        }
    }
}