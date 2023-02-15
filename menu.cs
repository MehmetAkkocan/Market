using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult Cikis;
            Cikis = MessageBox.Show("Uygulama Kapatılacak Emin misiniz?", "Kapatma Uyarısı!", MessageBoxButtons.YesNo);
            if (Cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
            if (Cikis == DialogResult.No)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            satis go = new satis();
            go.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            urunekle go = new urunekle();
            go.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            urunguncelle go = new urunguncelle();
            go.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            stokbilgi go = new stokbilgi();
            go.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            stokekle go = new stokekle();
            go.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            satisgecmis go = new satisgecmis();
            go.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            rapor go = new rapor();
            go.Show();
        }
    }
}
