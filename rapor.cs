using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market
{
    public partial class rapor : Form
    {
        public rapor()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(giris.bagla);
        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            dt.Clear();
            string zmn = dateTimePicker1.Value.ToString("MMM dd yyyy");
            con.Open();
            SqlCommand kmt0 = new SqlCommand("Select CIRO as Rapor,CIROtarih as Tarih From CIRO where CIROid =(Select CIROid From CIRO where CIROtarih like '%"+zmn+"%' )", con);
            SqlDataAdapter a1 = new SqlDataAdapter();
            a1.SelectCommand = kmt0;
            a1.Fill(dt);
            con.Close();
            dataGridView2.DataSource = dt;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            menu go = new menu();
            go.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.Clear();
            string zmn = dateTimePicker1.Value.ToString("MMM dd yyyy");
            con.Open();
            SqlCommand kmt0 = new SqlCommand("Select KAR as Rapor,KARtarih as Tarih From KAR where KARid =(Select KARid From KAR where KARtarih like '%"+zmn+"%' )", con);
            SqlDataAdapter a1 = new SqlDataAdapter();
            a1.SelectCommand = kmt0;
            a1.Fill(dt);
            con.Close();
            dataGridView2.DataSource = dt;
        }

        private void rapor_Load(object sender, EventArgs e)
        {            
            
        }
    }
}
