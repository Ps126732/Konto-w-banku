using System;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Bank;

namespace Bank.Tests
{
    [TestClass]
    public class KontoLimitTests
    {
        [TestMethod]
        public void Delegacja_PrawidlowoZwracaSrodkiDoDyspozycji()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 200, 300);

            
            Assert.AreEqual(500, konto.Bilans);
            Assert.AreEqual("Anna Nowak", konto.Nazwa);
        }

        [TestMethod]
        public void Wyplata_ZWykorzystaniemDebetu_DelegacjaBlokujeKonto()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 100, 200);

            konto.Wyplata(250); 

            Assert.IsTrue(konto.Zablokowane, "Delegowane konto powinno być zablokowane.");
            Assert.AreEqual(0, konto.Bilans, "Zablokowane konto ma 0 do dyspozycji.");
        }

        [TestMethod]
        public void Wyplata_PowyzejLimitu_RzucaWyjatek()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 100, 100);

            
            Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(300));
        }

        [TestMethod]
        public void Wplata_CalkowitaSplata_OdblokowujeDelegowaneKonto()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 50, 100);
            konto.Wyplata(100); 

            konto.Wplata(100); 

            Assert.IsFalse(konto.Zablokowane, "Konto powinno być odblokowane po spłacie.");
            Assert.AreEqual(150, konto.Bilans); 
        }
    }
}