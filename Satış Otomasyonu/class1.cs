using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satış_Otomasyonu
{
    public class Müsteri
    {
        public int müsteriId { get; set; }
        public string adres { get; set; }
        public string adi { get; set; }
    }
    public abstract class Odeme 

    {
        public double miktar { get; set; }
        public abstract void ucretOde(int tutar);
        public Odeme()
        {
         
        }
    }
    public class NakitOdeme : Odeme
    {
        int onay;
        public int Onay()
        {
            onay = 1;
            return onay;
        }
        public override void ucretOde(int topTutar)
        {
            miktar = topTutar;
        }
    }
    public class KartOdeme : Odeme
    {
        public decimal KartNO { get; set; }
        public string Tip { get; set; }
        public DateTime SKT { get; set; }
        int onay;
        public int Onay()
        {
            onay = 1;
            return onay;
        }
        public override void ucretOde(int topTutar)
        {
            miktar = topTutar;
        }
    }
    public class CekOdeme : Odeme
    {
        public string Isim { get; set; }
        public int BankID { get; set; }
        int onay;
        public int Onay()
        {
            onay = 1;
            return onay;
        }
        public override void ucretOde(int topTutar)
        {
            miktar = topTutar;
        }
    }
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int UrunStok { get; set; }
        public int UrunFiyat { get; set; }
        public int UrunKdv { get; set; }
        public int UrunSMiktar { get; set; }
        public double KargoAgırlık { get; set; }
        public string Aciklama { get; set; }
      
        public double GPFQ(int miktar)
        {
            return miktar * UrunFiyat;
        }
        public double GW()
        {
            return KargoAgırlık * UrunSMiktar;
        }
    }
    public class SiparisDetay
    {

        public Urun urun = new Urun();
        public double kdv { get; set; }
        public double tutar { get; set; }
        public double Aratoplam()
        {
            kdv = ((urun.GPFQ(urun.UrunSMiktar)) * urun.UrunKdv) / 100;
            return tutar = (urun.GPFQ(urun.UrunSMiktar) + kdv);
        }
        public double agirlikHesapla()
        {
            double agırlık = 0;
            return agırlık += urun.GW();
        }
    }
}

