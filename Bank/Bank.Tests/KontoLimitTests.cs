using System;
using Xunit;
using Bank;

namespace Bank.Tests
{
    public class KontoLimitTests
    {
        [Fact]
        public void Delegacja_PrawidlowoZwracaSrodkiDoDyspozycji()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 200, 300);
            Assert.Equal(500m, konto.Bilans);
            Assert.Equal("Anna Nowak", konto.Nazwa);
        }

        [Fact]
        public void Wyplata_ZWykorzystaniemDebetu_DelegacjaBlokujeKonto()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 100, 200);
            konto.Wyplata(250);

            Assert.True(konto.Zablokowane);
            Assert.Equal(0m, konto.Bilans);
        }

        [Fact]
        public void Wyplata_PowyzejLimitu_RzucaWyjatek()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 100, 100);
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(300));
        }

        [Fact]
        public void Wplata_CalkowitaSplata_OdblokowujeDelegowaneKonto()
        {
            KontoLimit konto = new KontoLimit("Anna Nowak", 50, 100);
            konto.Wyplata(100);
            konto.Wplata(100);

            Assert.False(konto.Zablokowane);
            Assert.Equal(150m, konto.Bilans);
        }
    }
}