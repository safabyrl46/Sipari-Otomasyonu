using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Satış_Otomasyonu
{

    public partial class mAnasayfa : Form
    {
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-IPUEIU5;Initial Catalog=SatisOtomasyonu;Integrated Security=True;MultipleActiveResultSets=True");

        public int ID;
        Urun urun = new Urun();

        public mAnasayfa()
        {
            InitializeComponent();
        }

        private void mAnasayfa_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'satisOtomasyonuDataSet2.sepet' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.sepetTableAdapter.Fill(this.satisOtomasyonuDataSet2.sepet);
            // TODO: Bu kod satırı 'satisOtomasyonuDataSet1.ürünler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.ürünlerTableAdapter.Fill(this.satisOtomasyonuDataSet1.ürünler);


            doldur();
            grid2();
            gridDoldur();
            
            

        }
        public void grid2()
        {
            //Sepetteki ürün fiyatlarını toplayıp labela yazdırdık.
            bag.Open();
            SqlCommand cmd1 = new SqlCommand("select * from sepet where musteriId=@d5", bag);
            cmd1.Parameters.AddWithValue("@d5", ID);
            SqlDataReader oku2 = cmd1.ExecuteReader();
            int sToplam = 0;
            while (oku2.Read())
            {
                
                sToplam +=Convert.ToInt32(oku2["topFiyat"]);
                label4.Text = sToplam.ToString();
            }
            oku2.Close();
            bag.Close();
        }
        public void gridDoldur()
        {
            //Datagridview'leri doldurmak için method kullanıyoruz.
            bag.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from ürünler",bag);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            //Sqlcommand kullanmadığımız için AddWidthValue kullanamadık.
            SqlDataAdapter da1 = new SqlDataAdapter("select * from sepet where musteriId='"+ID+"'", bag);
            DataTable table1 = new DataTable();
            da1.Fill(table1);
            dataGridView2.DataSource = table1;
            bag.Close();

        }
        public void doldur()
        {
            //Datagridviewde seçili olan ürünün fiyatını textbox1'e yazdırmak için metot kullanıyoruz.
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {          
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            int s1, s2, s3;
            string stok = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            string  id= dataGridView1.CurrentRow.Cells[0].Value.ToString();

            s1 = Convert.ToInt32(textBox2.Text);
            s2 = Convert.ToInt32(stok);
            //Sepete eklenen ürünün yeterli stoğu olup olmadığını kontrol ediyoruz.
            if (s1>s2)
            {
                MessageBox.Show("Maalesef Yeterli Stok Bulunmamaktadır.\nAlabileceğiniz Stok Miktarı:"+stok);
            }
            else
            {//Stokta var ise stoğu satın alınan miktarda azaltıp Ürünler tablosunda güncelliyoruz.
                bag.Open();
                s3 = s2 - s1;
                SqlCommand komut = new SqlCommand("update ürünler set stokMiktar=@s3 where id=@id ",bag);
                komut.Parameters.AddWithValue("@s3",s3);
                komut.Parameters.AddWithValue("@id", id);
                komut.ExecuteNonQuery();
                bag.Close();
            }
            bag.Open();
            //Giriş yapan müşterinin id numarası ile gerekli bilgileri tablodan çekiyoruz.
            SqlCommand komut1 = new SqlCommand("select * from müsteri where id=@d1",bag);
            komut1.Parameters.AddWithValue("@d1",ID);
            SqlDataReader oku = komut1.ExecuteReader();
            Müsteri müsteri1 = new Müsteri();
            //Çektiğim bilgileri müşteri classında yeni oluşturduğumuz nesneye atıyoruz.
            while (oku.Read())
            {
                müsteri1.müsteriId = ID;
                müsteri1.adi = oku["adi"].ToString();
                müsteri1.adres = oku["adresi"].ToString();
                oku.Close();
                break;
            }
           
            bag.Close();
            siparisEkle();
            NesneDoldur();
            gridDoldur();
            grid2();
           
           

        }
        public void NesneDoldur()
        {
            double top;
             bag.Open();
            //Seçilen ürünleri veritabanındaki Sepet tablosuna ekliyoruz.

            SqlCommand cmd = new SqlCommand("insert into sepet(urunAdi,urunMiktarı,topFiyat,musteriId,topAgirlik) values(@d1,@d2,@d3,@d4,@d5)", bag);
            //Datagridviewde seçili olan bilgileri nesnelere atıyoruz.
            urun.UrunID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            urun.UrunAdi = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            urun.UrunStok = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
            urun.UrunFiyat = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
            urun.UrunKdv = 10;
            urun.UrunSMiktar = Convert.ToInt32(textBox2.Text);
            urun.KargoAgırlık = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
            top = urun.GPFQ(urun.UrunSMiktar);
            cmd.Parameters.AddWithValue("@d1", urun.UrunAdi);
            cmd.Parameters.AddWithValue("@d2", urun.UrunSMiktar);
            cmd.Parameters.AddWithValue("@d3", top);
            cmd.Parameters.AddWithValue("@d4", ID);
            cmd.Parameters.AddWithValue("@d5", urun.GW());
            label5.Text = urun.GW().ToString();
            cmd.ExecuteNonQuery();
            bag.Close();
        }
        public void siparisEkle()
        {//Müşteri sepete ürün eklediği zaman o ürünün ve müşterinin bilgilerini verilenSiparisler tablosuna ekliyoruz.
            bag.Open();
            SqlCommand kmt = new SqlCommand("select * from müsteri where id=@d1", bag);
            kmt.Parameters.AddWithValue("@d1", ID);
            SqlDataReader read = kmt.ExecuteReader();
            SqlCommand kmt1 = new SqlCommand("insert into verilenSiparisler(müsteriID,müsteriAdi,ürünAdi,alinanMiktar) values(@0,@1,@2,@3)", bag);
            while (read.Read())
            {
                kmt1.Parameters.AddWithValue("@0",ID);
                kmt1.Parameters.AddWithValue("@1", read["adi"]);
                kmt1.Parameters.AddWithValue("@2", dataGridView1.CurrentRow.Cells[1].Value);
                kmt1.Parameters.AddWithValue("@3", textBox2.Text);
                
                kmt1.ExecuteNonQuery();
            }
            bag.Close();

        }
      
     
        private void button2_Click(object sender, EventArgs e)
        {//Çıkış butonuna basıldığı zaman müşteriyi ilk giriş ekranına yönlendiriyoruz.
            ANASAYFA form = new ANASAYFA();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Satın al butonuna basıldığında ödeme formunu açıyoruz.
            odeme form = new odeme();
            //Bu form üzerinden gerekli bilgilerini gönderiyoruz.
            form.Toplam = label4.Text;
            form.agirlik = Convert.ToInt32(label5.Text);
            form.ID = ID;
            form.Show();
            
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Sepeti temizle butonuna basıldığında müşterinin id'si ile sepet tablosunda eşleşen kayıtları siliyoruz.
            bag.Open();
            SqlCommand komut = new SqlCommand("Delete from sepet where musteriId=@d1",bag);
            komut.Parameters.AddWithValue("@d1",ID);
            komut.ExecuteNonQuery();
            bag.Close();
            gridDoldur();
            grid2();
            bag.Open();
            SqlCommand kmt = new SqlCommand("delete from verilenSiparisler where müsteriID=@d2",bag);
            kmt.Parameters.AddWithValue("@d2",ID);
            kmt.ExecuteNonQuery();
            bag.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Datagridviewde seçili olan ürünü siliyoruz.
            bag.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where id=@d1",bag);
            komut.Parameters.AddWithValue("@d1", dataGridView2.CurrentRow.Cells[0].Value.ToString()) ;
            komut.ExecuteNonQuery();


            bag.Close();
            bag.Open();
            SqlCommand cmd = new SqlCommand("delete from verilenSiparisler where ürünAdi=@d2",bag);
            cmd.Parameters.AddWithValue("@d2", dataGridView2.CurrentRow.Cells[1].Value.ToString());
            cmd.ExecuteNonQuery();
            bag.Close();
            gridDoldur();
            grid2();


        }
    }
}
