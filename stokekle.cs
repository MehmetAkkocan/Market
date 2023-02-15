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

namespace Market
{
    public partial class stokekle : Form
    {
        public stokekle()
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
        private void tbbarkot_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "";
            con.Open();
            SqlCommand kmt0 = new SqlCommand("Select URUNadi From URUN  where URUNid like '" + tbbarkod.Text + "' ", con);
            SqlDataReader a = kmt0.ExecuteReader();
            while (a.Read())
            {
                label3.Text = a["URUNadi"].ToString();
                
            }
            con.Close();
        }
        public static int tpx;
        private void button8_Click(object sender, EventArgs e)
        {
             
            con.Open();
            SqlCommand kmt1 = new SqlCommand("Select URUNadet From URUN where URUNid like '" + tbbarkod.Text + "'", con);
            SqlDataReader a1 = kmt1.ExecuteReader();
            while (a1.Read())
            {
                tpx = Convert.ToInt32(a1["URUNadet"]);
            }
            con.Close();
            tpx += int.Parse(tbadet.Text);
            con.Open();
            SqlCommand kmt2 = new SqlCommand("Update URUN Set URUNadet = @adet Where URUNid = @uid", con);
            kmt2.Parameters.AddWithValue("@uid", int.Parse(tbbarkod.Text));            
            kmt2.Parameters.AddWithValue("@adet", tpx);
            kmt2.ExecuteNonQuery();
            con.Close();
            con.Open();
            SqlCommand kmt4 = new SqlCommand("Select u.URUNid as BarkodNo , u.URUNadi as Ürün, m.MARKAadi as Marka, u.URUNadet as Adet From URUN u, MARKA m where u.MARKAid = m.MARKAid and u.URUNid = @uid ", con);
            kmt4.Parameters.AddWithValue("@uid", int.Parse(tbbarkod.Text));
            SqlDataReader a2 = kmt4.ExecuteReader();
            while (a2.Read())
            {
                dt.Rows.Add(a2["BarkodNo"], a2["Ürün"], a2["Marka"], a2["Adet"]);
            }
            con.Close();
            tbbarkod.Text = "";
            tbadet.Text = "";
            label3.Text = "";
        }
        
        private void stokekle_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("BarkodNo", typeof(int));
            dt.Columns.Add("Ürün", typeof(string));
            dt.Columns.Add("Marka", typeof(string));
            dt.Columns.Add("Adet", typeof(int));
            dataGridView2.DataSource = dt;
        }
    }
}
