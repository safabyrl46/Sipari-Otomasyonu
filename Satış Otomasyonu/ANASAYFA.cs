using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Satış_Otomasyonu
{
    public partial class ANASAYFA : Form
    {
        public ANASAYFA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmLogin form = new FrmLogin();
            this.Hide();
            form.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            adminLogin form = new adminLogin();
            this.Hide();
            form.Show();
        }

        private void ANASAYFA_Load(object sender, EventArgs e)
        {

        }
    }
}
