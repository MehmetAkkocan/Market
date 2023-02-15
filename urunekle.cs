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
    public partial class urunekle : Form
    {
        public urunekle()
        {
            InitializeComponent();
            MarkaGetir();
        }
        SqlConnection con = new SqlConnection(giris.bagla);
        DataTable dt = new DataTable();
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            menu go = new menu();
            go.Show();
        }
        private void Temizle()
        {
            tbbarkot.Text = "";
            tburun.Text = "";
            tbadet.Text = "";
            tbfiyat.Text = "";
            tbsatfiyat.Text = "";
            cbmarka.Text = "";
        }
        private void MarkaGetir()
        {
            con.Open();
            SqlCommand kmt = new SqlCommand("Select * From MARKA Order By MARKAid", con);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                cbmarka.Items.Add(dr["MARKAadi"].ToString());
            }
            con.Close();
        }
        public static int y;
        private void button8_Click(object sender, EventArgs e)
        {
            if (tbbarkot.Text!="" && tbadet.Text!="" && tburun.Text!="" && tbfiyat.Text!="" && tbsatfiyat.Text!="" && cbmarka.Text!="")
            {
                con.Open();
                SqlCommand kmt3 = new SqlCommand("Select * FROM URUN Where URUNid = @uid", con);
                kmt3.Parameters.AddWithValue("@uid", int.Parse(tbbarkot.Text));
                SqlDataReader d = kmt3.ExecuteReader();
                Boolean c = d.Read();
                con.Close();
                if (c == true)
                {
                    MessageBox.Show("Bu Barkot Numarasına Sahip Bir Ürün Zaten Var!");
                }
                else
                {


                    con.Open();
                    SqlCommand kmt0 = new SqlCommand("Select * FROM MARKA Where MARKAadi = @madi", con);
                    kmt0.Parameters.AddWithValue("@madi", cbmarka.Text);
                    SqlDataReader a = kmt0.ExecuteReader();
                    Boolean b = a.Read();
                    con.Close();
                    if (b == true)
                    {

                    }
                    else
                    {
                        con.Open();
                        SqlCommand mekle = new SqlCommand("Insert into MARKA(MARKAadi) values(@m) ", con);
                        mekle.Parameters.AddWithValue("@m", cbmarka.Text);
                        mekle.ExecuteNonQuery();
                        con.Close();
                    }
                    con.Open();
                    SqlCommand kmt2 = new SqlCommand("Select MARKAid FROM MARKA Where MARKAadi = @mad", con);
                    kmt2.Parameters.AddWithValue("@mad", cbmarka.Text);
                    SqlDataReader a1 = kmt2.ExecuteReader();

                    while (a1.Read())
                    {
                        y = Convert.ToInt32(a1["MARKAid"]);
                    }
                    a1.Close();
                    con.Close();
                    con.Open();
                    SqlCommand kmt1 = new SqlCommand("Insert into URUN(URUNid,URUNadi,URUNadet,URUNalfiyat,URUNsatfiyat,MARKAid) values(@id,@adi,@adet,@alfyt,@satfyt,@mid)", con);
                    kmt1.Parameters.AddWithValue("@id", int.Parse(tbbarkot.Text));
                    kmt1.Parameters.AddWithValue("@adi", tburun.Text);
                    kmt1.Parameters.AddWithValue("@adet", int.Parse(tbadet.Text));
                    kmt1.Parameters.AddWithValue("@alfyt", double.Parse(tbfiyat.Text));
                    kmt1.Parameters.AddWithValue("@satfyt", double.Parse(tbsatfiyat.Text));
                    kmt1.Parameters.AddWithValue("@mid", y);
                    kmt1.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    SqlCommand kmt4 = new SqlCommand("Select u.URUNid as BarkotNo , u.URUNadi as Ürün, u.URUNadet as Adet, u.URUNsatfiyat as SatışFiyatı, m.MARKAadi as Marka From URUN u, MARKA m where u.MARKAid = m.MARKAid and u.URUNid = @id ", con);
                    kmt4.Parameters.AddWithValue("@id", int.Parse(tbbarkot.Text)); 
                    SqlDataReader a2 = kmt4.ExecuteReader();
                    while (a2.Read())
                    {
                        dt.Rows.Add(a2["BarkotNo"], a2["Ürün"], a2["Adet"], a2["SatışFiyatı"], a2["Marka"]);
                    }
                    con.Close();
                    label8.Text = "Ürün Eklendi";
                    Temizle();
                }
            }
            else
            {
                MessageBox.Show("Alanlar Boş Bırakılamaz!");
            }
            
        }

        private void urunekle_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("BarkotNo", typeof(int));
            dt.Columns.Add("Ürün", typeof(string));
            dt.Columns.Add("Adet", typeof(int));
            dt.Columns.Add("SatışFiyatı", typeof(double));
            dt.Columns.Add("Marka", typeof(string));
            dataGridView2.DataSource = dt;
        }
    }
}
