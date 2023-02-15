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
using System.Security.Cryptography;

namespace Market
{
    public partial class satisgecmis : Form
    {
        public satisgecmis()
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
        private void button8_Click(object sender, EventArgs e)
        {
            if (tbbarkod.Text == "")
            {

            }
            else
            {
                con.Open();
                SqlCommand kmt4 = new SqlCommand("Select s.SATtarih as SatışTarihi, s.SATtoplam as SatışToplam, o.Oadi as ÖdemeTipi, k.KASAadi as KasaAdı From SATIS s, ODEME o, KASA k where o.Oid = s.Oid and s.KASAid=k.KASAid and SATid =@stid", con);
                kmt4.Parameters.AddWithValue("@stid", int.Parse(tbbarkod.Text));
                SqlDataAdapter a1 = new SqlDataAdapter();
                a1.SelectCommand = kmt4;
                a1.Fill(dt);
                con.Close();
                dataGridView2.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string zmn = dateTimePicker1.Value.ToString("MMM dd yyyy");
            con.Open();
            SqlCommand kmt0 = new SqlCommand("Select s.SATtarih as SatışTarihi, s.SATtoplam as SatışToplam, o.Oadi as ÖdemeTipi, k.KASAadi as KasaAdı From SATIS s, ODEME o, KASA k where o.Oid = s.Oid and s.KASAid=k.KASAid and SATtarih like '%" + zmn + "%'", con);
            SqlDataAdapter a1 =new SqlDataAdapter();
            a1.SelectCommand =kmt0;
            a1.Fill(dt);      
            con.Close();           
            dataGridView2.DataSource = dt;
        }

        private void satisgecmis_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("SatışTarihi", typeof(DateTime));
            dt.Columns.Add("SatışToplam", typeof(double));
            dt.Columns.Add("ÖdemeTipi", typeof(string));
            dt.Columns.Add("KasaAdı", typeof(string));
            dataGridView2.DataSource = dt;
        }
    }
}
