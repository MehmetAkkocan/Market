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
    public partial class stokbilgi : Form
    {
        public stokbilgi()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            menu go = new menu();
            go.Show();
        }
        SqlConnection con = new SqlConnection(giris.bagla);
        DataTable dt = new DataTable();
        private void button3_Click(object sender, EventArgs e)
        {
            if (tbbarkot.Text == "")
            {

            }
            else
            {
                dt.Clear();
                con.Open();
                SqlCommand kmt0 = new SqlCommand("Select u.URUNadi as ÜrünAdı,m.MARKAadi as Marka, u.URUNadet as ÜrünAdeti From URUN u, MARKA m Where u.MARKAid=m.MARKAid and u.URUNid=@uid ", con);
                kmt0.Parameters.AddWithValue("@uid", int.Parse(tbbarkot.Text));
                SqlDataAdapter a1 = new SqlDataAdapter();
                a1.SelectCommand = kmt0;
                a1.Fill(dt);
                con.Close();
                dataGridView2.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dt.Clear();
            con.Open();
            SqlCommand kmt1 = new SqlCommand("Select u.URUNadi as ÜrünAdı,m.MARKAadi as Marka, u.URUNadet as ÜrünAdeti From URUN u, MARKA m Where u.MARKAid=m.MARKAid and u.URUNadet <= 10  ", con);
            SqlDataAdapter a1 = new SqlDataAdapter();
            a1.SelectCommand = kmt1;
            a1.Fill(dt);
            con.Close();
            dataGridView2.DataSource = dt;
        }
    }
}
