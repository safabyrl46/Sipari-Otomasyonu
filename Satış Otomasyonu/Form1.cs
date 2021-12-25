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
    public partial class FrmLogin : Form
    {
        
        bool durum;
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-IPUEIU5;Initial Catalog=SatisOtomasyonu;Integrated Security=True");

        public FrmLogin()
        {
            InitializeComponent();
            mSifre.PasswordChar = '*';
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmKayit form = new frmKayit();
            this.Hide();
            form.Show();    
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();
            string kadi, sifre;
            int id;
            kadi = mKadi.Text;
            sifre = mSifre.Text;
            SqlCommand komut = new SqlCommand("select * from müsteri",bag);
            SqlDataReader oku = komut.ExecuteReader();
            //Kullanıcı adı eşleşen müşterinin id numarasını tablodan çekiyoruz.
            SqlCommand cmd = new SqlCommand("select * from müsteri where kadi=@d2", bag);
            cmd.Parameters.AddWithValue("@d2",kadi);
            
            mAnasayfa form = new mAnasayfa();
          
            while (oku.Read())
            {//Girilen kullanıcı adı ve şifre veritabanında eşleşirse durum değişkenine true değeri atıyoruz.
                if (kadi==oku["kadi"].ToString() && sifre==oku["sifre"].ToString())
                {
                    durum = true;
                    break;
                }
                else
                {
                    durum = false;
                }
                
            }
            oku.Close();
            //Giriş yapan müşterinin id numarasını bir sonraki forma gönderiyoruz.
            SqlDataReader oku2 = cmd.ExecuteReader();
            while (oku2.Read())
            {
                id = Convert.ToInt32(oku2["id"]);
                form.ID = id;
                
            }
            oku2.Close();
            //Durum değişkeni true ise başarıyla giriş yapılıyor ve müşteri anasayfa formuna yönlendiriliyor.
            if (durum)
            {
               
                this.Hide();
                form.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Girişi...");
            }
            bag.Close();
        }

       
    }
    
}
