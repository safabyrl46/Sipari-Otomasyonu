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
    

    public partial class odeme : Form
    {
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-IPUEIU5;Initial Catalog=SatisOtomasyonu;Integrated Security=True;MultipleActiveResultSets=True");
        public int authorized;
        public int ID;
        public int agirlik;
        //Ödeme bölümü için ödeme 3 adet nesne oluşturuyoruz.
        NakitOdeme nakit = new NakitOdeme();
        KartOdeme kart = new KartOdeme();
        CekOdeme cek = new CekOdeme();
       

        public string Toplam;
        public odeme()
        {
            InitializeComponent();
        }

        private void odeme_Load(object sender, EventArgs e)
        {
            //Form yüklendiği zaman form elemanlarını false duruma getiriyoruz.
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            dateTimePicker1.Enabled = false;
            //Önceki formdan gelen toplam bilgisine kdv ekleyip label2'ye yazdırıyoruz.
            int kdv = (Convert.ToInt32(Toplam) * 10) / 100;
            int kdvDahil = Convert.ToInt32(Toplam) + kdv;
            label2.Text = kdvDahil.ToString();
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Combobox üzerinden seçili olan bölümü form üzerinde true duruma getiriyoruz.
            if (comboBox1.Text=="Kredi Kartı")
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;

            }
            else if (comboBox1.Text == "Nakit Ödeme")
            {
                textBox3.Enabled = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                dateTimePicker1.Enabled = false;
                textBox3.Text = label2.Text;
            }
            else if (comboBox1.Text == "Çek")
            {
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void menuyeDon()
        {
            mAnasayfa form = new mAnasayfa();
            this.Hide();
            form.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Combobox'ta seçili olan ödeme yöntemine göre belirli metotlar çağrılıp işlem yapılıyor.
            if (comboBox1.Text == "Nakit Ödeme")
            {
                authorized = nakit.Onay();
                nakit.ucretOde(Convert.ToInt32(label2.Text));
                
                if (authorized==1)
                {
                    MessageBox.Show("ÖDEME BAŞARILI");
                }
                

            }
            else if (comboBox1.Text == "Kredi Kartı")
            {
                authorized = kart.Onay();
                kart.ucretOde(Convert.ToInt32(label2.Text));
                kart.KartNO = Convert.ToDecimal(textBox1.Text);
                kart.Tip = textBox2.Text;
                kart.SKT = Convert.ToDateTime(dateTimePicker1.Text);
                if (authorized==1)
                {
                    MessageBox.Show("ÖDEME BAŞARILI");
                }
                
            }
            else if (comboBox1.Text == "Çek")
            {
                authorized = cek.Onay();
                cek.ucretOde(Convert.ToInt32(label2.Text));
              
                cek.Isim = textBox4.Text;
                cek.BankID = Convert.ToInt32(textBox5.Text);
                if (authorized==1)
                {
                    MessageBox.Show("ÖDEME BAŞARILI");
                }
 
            }
            //Ödenen ürün bilgilerini verilenSiparisler tablosuna ekliyoruz.
            bag.Open();
            SqlCommand cmd = new SqlCommand("update verilenSiparisler set odemeTipi=@d1,ödenenUcret=@d2,kargoAgirligi=@d4 where müsteriID=@d3",bag);
            cmd.Parameters.AddWithValue("@d1",comboBox1.Text);
            cmd.Parameters.AddWithValue("@d2",Convert.ToInt32(label2.Text));
            cmd.Parameters.AddWithValue("@d3", ID);
            cmd.Parameters.AddWithValue("@d4",agirlik);
            cmd.ExecuteNonQuery();
            bag.Close();
            this.Hide();

        }
    }
}
