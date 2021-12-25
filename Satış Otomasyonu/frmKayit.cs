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
    public partial class frmKayit : Form
    {
        
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-IPUEIU5;Initial Catalog=SatisOtomasyonu;Integrated Security=True");
        public frmKayit()
        {
  
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bag.Open();        
            SqlCommand komut = new SqlCommand("insert into müsteri(kadi,sifre,adi,adresi) values(@p1,@p2,@p3,@p4)",bag);
            komut.Parameters.AddWithValue("@p1",kadi.Text);
            komut.Parameters.AddWithValue("@p2", sifre.Text);
            komut.Parameters.AddWithValue("@p3", adi.Text);
            komut.Parameters.AddWithValue("@p4", adresi.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Başarıyla Kayıt Oldunuz...");
            bag.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            FrmLogin form = new FrmLogin();
            this.Hide();
            form.Show();
        }
    }
}
