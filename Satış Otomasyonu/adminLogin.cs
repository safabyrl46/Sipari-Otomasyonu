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
    public partial class adminLogin : Form
    {
        bool durum;
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-IPUEIU5;Initial Catalog=SatisOtomasyonu;Integrated Security=True");
        public adminLogin()
        {
            InitializeComponent();
            ySifre.PasswordChar = '*';
        }

        private void adminLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bag.Open();
            string kadi, sifre;
            kadi = yKadi.Text;
            sifre = ySifre.Text;
            SqlCommand komut = new SqlCommand("select * from admin", bag);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {//Girilen kullanıcı adı ve şifre veritabanında eşleşirse durum değişkenine true değeri atıyoruz.
                if (kadi == oku["kadi"].ToString() && sifre == oku["sifre"].ToString())
                {
                    durum = true;
                    break;
                }
                else
                {
                    durum = false;
                }
            }
            //Durum değişkeni true ise başarıyla giriş yapılıyor ve yönetici anasayfa formuna yönlendiriliyor.
            if (durum)
            {
                aAnasayfa form = new aAnasayfa();
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
