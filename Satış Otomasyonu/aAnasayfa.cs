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
    public partial class aAnasayfa : Form
    {
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-IPUEIU5;Initial Catalog=SatisOtomasyonu;Integrated Security=True");


        public aAnasayfa()
        {
            InitializeComponent();
        }

        private void aAnasayfa_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'satisOtomasyonuDataSet3.verilenSiparisler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.verilenSiparislerTableAdapter.Fill(this.satisOtomasyonuDataSet3.verilenSiparisler);
            // TODO: Bu kod satırı 'satisOtomasyonuDataSet.ürünler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.ürünlerTableAdapter.Fill(this.satisOtomasyonuDataSet.ürünler);



            gridDoldur();

        }
        public void gridDoldur()
        {//Datagridview'leri doldurmak için method kullanıyoruz.
            bag.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from ürünler", bag);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
            SqlDataAdapter da1 = new SqlDataAdapter("select * from verilenSiparisler  ", bag);
            DataTable table1 = new DataTable();
            da1.Fill(table1);
            dataGridView2.DataSource = table1;
            bag.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Yöneticiden alınan ürün bilgilerini Ürünler tablosuna ekliyoruz.
            bag.Open();
            SqlCommand komut = new SqlCommand("insert into ürünler(ürünAdi,ürünFiyati,stokMiktar,ürünAgirlik) values(@d1,@d2,@d3,@d4)",bag);
            komut.Parameters.AddWithValue("@d1",textBox1.Text);
            komut.Parameters.AddWithValue("@d2", textBox2.Text);
            komut.Parameters.AddWithValue("@d3", textBox3.Text);
            komut.Parameters.AddWithValue("@d4",textBox4.Text);
            komut.ExecuteNonQuery();
            bag.Close();
            gridDoldur();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridview'in seçili satır bilgilerini textbox'lara yazdırıyoruz.
            textBox1.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Datagridviewde seçili olan ürünü Ürünler tablosundan siliyoruz.
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            bag.Open();
            SqlCommand komut = new SqlCommand("delete from ürünler where id=@d1",bag);
            komut.Parameters.AddWithValue("@d1",id);
            komut.ExecuteNonQuery();
            bag.Close();
            gridDoldur();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Datagridviewde seçili olan ürünün bilgilerini Ürünler tablosundan güncelliyoruz.
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            bag.Open();
            SqlCommand komut = new SqlCommand("update ürünler set ürünAdi=@d1, ürünFiyati=@d2, stokMiktar=@d3, ürünAgirlik=@d5 where id=@d4",bag);
            komut.Parameters.AddWithValue("@d1", textBox1.Text);
            komut.Parameters.AddWithValue("@d2", textBox2.Text);
            komut.Parameters.AddWithValue("@d3", textBox3.Text);
            komut.Parameters.AddWithValue("@d4", id);
            komut.Parameters.AddWithValue("@d5", textBox4.Text);
            komut.ExecuteNonQuery();
            bag.Close();
            gridDoldur();
            
        }
    }
}
