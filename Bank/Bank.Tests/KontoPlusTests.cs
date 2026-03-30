using System;
using Xunit;
using Bank;

namespace Bank.Tests
{
    public class KontoPlusTests
    {
        [Fact]
        public void Bilans_ZwracaSrodkiDoDyspozycji()
        {
            KontoPlus konto = new KontoPlus("Jan Kowalski", 100, 200);
            Assert.Equal(300m, konto.Bilans);
        }

        [Fact]
        public void Wyplata_ZWykorzystaniemDebetu_BlokujeKonto()
        {
            KontoPlus konto = new KontoPlus("Jan Kowalski", 100, 200);
            konto.Wyplata(250);

            Assert.True(konto.Zablokowane);
            Assert.Equal(0m, konto.Bilans);
        }

        [Fact]
        public void Wyplata_PrzekroczenieLimitu_RzucaWyjatek()
        {
            KontoPlus konto = new KontoPlus("Jan Kowalski", 100, 50);
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [Fact]
        public void Wplata_CzesciowaSplataDebetu_NieOdblokowujeKonta()
        {
            KontoPlus konto = new KontoPlus("Jan Kowalski", 0, 100);
            konto.Wyplata(100);
            konto.Wplata(50);

            Assert.True(konto.Zablokowane);
        }

        [Fact]
        public void Wplata_CalkowitaSplataDebetu_OdblokowujeKontoOrazPrzywracaLimit()
        {
            KontoPlus konto = new KontoPlus("Jan Kowalski", 50, 100);
            konto.Wyplata(100);
            konto.Wplata(60);

            Assert.False(konto.Zablokowane);
            Assert.Equal(110m, konto.Bilans);
        }
    }
}