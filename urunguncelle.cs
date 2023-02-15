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
    public partial class urunguncelle : Form
    {
        public urunguncelle()
        {
            InitializeComponent();
        }
        private void Temizle()
        {
            tburun.Text = "";
            tbadet.Text = "";
            tbfiyat.Text = "";
            tbsatfiyat.Text = "";
            cbmarka.Text = "";
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
            Temizle();
            con.Open();
            SqlCommand kmt0 = new SqlCommand("Select u.URUNadi, u.URUNadet, u.URUNalfiyat, u.URUNsatfiyat, m.MARKAadi From URUN u, Marka m where u.MARKAid=m.MARKAid and URUNid like '"+tbbarkot.Text +"' ",con);
            SqlDataReader a = kmt0.ExecuteReader();
            while (a.Read())
            {
                tburun.Text = a["URUNadi"].ToString();
                tbadet.Text = a["URUNadet"].ToString();
                tbfiyat.Text = a["URUNalfiyat"].ToString();
                tbsatfiyat.Text = a["URUNsatfiyat"].ToString();
                cbmarka.Text = a["MARKAadi"].ToString();
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand kmt1 = new SqlCommand("Update URUN Set URUNadi = @adi, URUNadet = @adet, URUNalfiyat = @alfyt, URUNsatfiyat = @satfyt, MARKAid = (Select MARKAid From MARKA Where MARKAadi = @mid) Where URUNid = @uid",con);
            kmt1.Parameters.AddWithValue("@uid",int.Parse(tbbarkot.Text));
            kmt1.Parameters.AddWithValue("@adi",tburun.Text);
            kmt1.Parameters.AddWithValue("@adet",int.Parse(tbadet.Text));
            kmt1.Parameters.AddWithValue("@alfyt",double.Parse(tbfiyat.Text));
            kmt1.Parameters.AddWithValue("@satfyt",double.Parse(tbsatfiyat.Text));
            kmt1.Parameters.AddWithValue("@mid",cbmarka.Text);
            kmt1.ExecuteNonQuery();
            con.Close();
            con.Open();
            SqlCommand kmt4 = new SqlCommand("Select u.URUNid as BarkotNo , u.URUNadi as Ürün, u.URUNadet as Adet, u.URUNalfiyat as AlışFiyatı, u.URUNsatfiyat as SatışFiyatı, m.MARKAadi as Marka From URUN u, MARKA m where u.MARKAid = m.MARKAid and u.URUNid = @id ", con);
            kmt4.Parameters.AddWithValue("@id", int.Parse(tbbarkot.Text));
            SqlDataReader a2 = kmt4.ExecuteReader();
            while (a2.Read())
            {
                dt.Rows.Add(a2["BarkotNo"], a2["Ürün"], a2["Adet"], a2["AlışFiyatı"], a2["SatışFiyatı"], a2["Marka"]);
            }
            con.Close();
            Temizle();
            tbbarkot.Text = "";
        }

        private void urunguncelle_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("BarkotNo", typeof(int));
            dt.Columns.Add("Ürün", typeof(string));
            dt.Columns.Add("Adet", typeof(int));
            dt.Columns.Add("AlışFiyatı", typeof(double));
            dt.Columns.Add("SatışFiyatı", typeof(double));
            dt.Columns.Add("Marka", typeof(string));
            dataGridView2.DataSource = dt;
        }
    }
}
