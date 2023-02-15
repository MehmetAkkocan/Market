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
using System.Data.Common;
using System.Net.NetworkInformation;

namespace Market
{
    public partial class satis : Form
    {
        public satis()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult Cikis;
            Cikis = MessageBox.Show("Menüye Dönülecek Emin misiniz?", "Kapatma Uyarısı!", MessageBoxButtons.YesNo);
            if (Cikis == DialogResult.Yes)
            {
                this.Close();
                menu go = new menu();
                go.Show();
            }
            if (Cikis == DialogResult.No)
            {

            }
        }
        SqlConnection con = new SqlConnection(giris.bagla);
        DataTable dt = new DataTable();
        public static double gtop = 0;
        private void button8_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand kmt4 = new SqlCommand("Select u.URUNid as BarkodNo , u.URUNadi as Ürün, u.URUNsatfiyat as SatışFiyatı, m.MARKAadi as Marka From URUN u, MARKA m where u.MARKAid = m.MARKAid and u.URUNid = @id ", con);
            kmt4.Parameters.AddWithValue("@id", int.Parse(tbbarkod.Text));
            SqlDataReader a2 = kmt4.ExecuteReader();
            while (a2.Read())
            {
                dt.Rows.Add(a2["BarkodNo"], a2["Ürün"], a2["Marka"], int.Parse(tbadet.Text), double.Parse(tbtoplam.Text));
            }
            con.Close();
            double b = double.Parse(tbtoplam.Text);
            gtop += b;
            label7.Text = "GENEL TOPLAM : " + gtop + "TL";
        }        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            tburun.Text = "";
            tbfiyat.Text = "";
            tbadet.Text = "";
            tbtoplam.Text = "";
            SqlCommand kmt0 = new SqlCommand("Select URUNadi, URUNsatfiyat From URUN where URUNid like '" + tbbarkod.Text + "' ", con);
            SqlDataReader a = kmt0.ExecuteReader();
            while (a.Read())
            {
                tburun.Text = a["URUNadi"].ToString();                
                tbfiyat.Text = a["URUNsatfiyat"].ToString();                
            }
            con.Close();
        }
        static bool Kontrol(string deger)
        {
            try
            {
                int.Parse(deger);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void tbadet_TextChanged(object sender, EventArgs e)
        {
            if ( (Kontrol(tbadet.Text)))
            {
                double x = double.Parse(tbadet.Text) * double.Parse(tbfiyat.Text);
                tbtoplam.Text = x.ToString();
            }
            
        }

        private void satis_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("BarkodNo", typeof(int));
            dt.Columns.Add("Ürün", typeof(string));
            dt.Columns.Add("Marka", typeof(string));
            dt.Columns.Add("Adet", typeof(int));
            dt.Columns.Add("SatışFiyatı", typeof(double));
            dataGridView2.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int b = int.Parse(dataGridView2.SelectedRows[0].Cells[4].Value.ToString());
                dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
                gtop -= b;
                label7.Text = "GENEL TOPLAM : " + gtop + " TL";
            }
            else
            {
                MessageBox.Show("Lüffen silinecek satırı seçin.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tburun.Text = "";
            tbfiyat.Text = "";
            tbadet.Text = "";
            tbtoplam.Text = "";
            tbbarkod.Text = "";
            dt.Clear();
            label7.Text = "GENEL TOPLAM : 0 TL";
        }
        int fid;
        double ciro;
        double kar;
        double alfyt;
        double stfyt;
        double urunkar;
        int stok;
        private void btnsat_Click(object sender, EventArgs e)
        {
            DialogResult sat;
            sat = MessageBox.Show("Satışı Onaylıyor musunuz ?", "Satış Uyarısı!", MessageBoxButtons.YesNo);
            if (sat == DialogResult.Yes)
            {
                con.Open();
                SqlCommand kmt0 = new SqlCommand("Select MAX(FATid) From SAT_URUN ", con);
                fid = Convert.ToInt32(kmt0.ExecuteScalar());
                con.Close();
                
                for (int i = 0; i < dt.Rows.Count ; i++)
                {
                    int urid = Convert.ToInt32(dt.Rows[i]["BarkodNo"]);
                    int fadet = Convert.ToInt32(dt.Rows[i]["Adet"]);
                    double satfyt = Convert.ToDouble(dt.Rows[i]["SatışFiyatı"]);
                    con.Open();
                    SqlCommand kmt1 = new SqlCommand("Insert into SAT_URUN(FATid,URUNid,FATadet,URUNsatfiyatı) Values(@fatid,@uid,@adet,@fiyat)", con);
                    kmt1.Parameters.AddWithValue("@fatid", (fid + 1));
                    kmt1.Parameters.AddWithValue("@uid", urid);
                    kmt1.Parameters.AddWithValue("@adet", fadet);
                    kmt1.Parameters.AddWithValue("@fiyat", satfyt);
                    kmt1.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    SqlCommand kmtalfyt = new SqlCommand("Select URUNalfiyat,URUNsatfiyat From URUN Where URUNid=@brkd",con);
                    kmtalfyt.Parameters.AddWithValue("@brkd",urid);
                    SqlDataReader urd = kmtalfyt.ExecuteReader();
                    while (urd.Read())
                    {
                        stfyt = Convert.ToInt32(urd["URUNsatfiyat"]);
                        alfyt = Convert.ToInt32(urd["URUNalfiyat"]);
                        urunkar = (stfyt - alfyt) * fadet;
                    }
                                        
                    con.Close();
                    con.Open();
                    SqlCommand kmtstk = new SqlCommand("Select URUNadet From URUN Where URUNid=@uid", con);
                    kmtstk.Parameters.AddWithValue("@uid", urid);
                    SqlDataReader urd1 = kmtstk.ExecuteReader();
                    while (urd1.Read())
                    {
                        stok = Convert.ToInt32(urd1["URUNadet"]);
                        stok -= fadet;
                    }
                    
                    con.Close();
                    con.Open();
                    SqlCommand kmtstokdus = new SqlCommand("Update URUN Set URUNadet =@adet Where URUNid =@urid", con);
                    kmtstokdus.Parameters.AddWithValue("@adet",stok);
                    kmtstokdus.Parameters.AddWithValue("@urid",urid);
                    con.Close();
                }
                int odemeid;
                if (radioButton1.Checked == true)
                {
                    odemeid = 1;
                }
                else
                {
                    odemeid = 2;
                }
                int kasaid =0;
                con.Open();
                SqlCommand kmt3 = new SqlCommand("Select KASAid From KASA Where KASAadi =@kadi", con);
                kmt3.Parameters.AddWithValue("kadi", giris.kasaadi);
                SqlDataReader ka = kmt3.ExecuteReader();
                while (ka.Read())
                {
                    kasaid = Convert.ToInt32(ka["KASAid"]);
                }
                con.Close();
                con.Open();
                SqlCommand kmt2 = new SqlCommand("Insert into SATIS(SATtoplam,FATid,Oid,KASAid) values (@stplm,@fid,@oid,@kid)", con);
                kmt2.Parameters.AddWithValue("stplm", gtop);
                kmt2.Parameters.AddWithValue("fid", (fid+1));
                kmt2.Parameters.AddWithValue("oid", odemeid);                               
                kmt2.Parameters.AddWithValue("kid", kasaid);
                kmt2.ExecuteNonQuery();
                con.Close();
                con.Open();
                SqlCommand kmt4 = new SqlCommand("Select CIROtarih From CIRO Where CIROtarih = @cirotar",con);
                kmt4.Parameters.AddWithValue("@cirotar", DateTime.Today.ToShortDateString());
                SqlDataReader cdr = kmt4.ExecuteReader();
                
                if (cdr.Read())
                {
                    con.Close();
                    con.Open();
                    SqlCommand kmt5 = new SqlCommand("Select CIRO From CIRO Where CIROtarih=@cirotar",con);
                    kmt5.Parameters.AddWithValue("@cirotar", DateTime.Today.ToShortDateString());
                    SqlDataReader cda = kmt5.ExecuteReader();
                    while (cda.Read())
                    {
                        ciro = Convert.ToInt32(cda["CIRO"]);
                        ciro += gtop;
                    }
                    
                    con.Close();
                    con.Open();
                    SqlCommand kmt6 = new SqlCommand("Update CIRO Set CIRO =@ciro Where CIROid=(Select CIROid From CIRO Where CIROtarih=@cirotar)", con);
                    kmt6.Parameters.AddWithValue("@ciro", ciro);
                    kmt6.Parameters.AddWithValue("@cirotar", DateTime.Today.ToShortDateString());
                    kmt6.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    con.Close();
                    con.Open();
                    SqlCommand kmt7 = new SqlCommand("Insert into CIRO(CIRO) Values(@ciro)", con);
                    kmt7.Parameters.AddWithValue("@ciro", gtop);
                    kmt7.ExecuteNonQuery();
                    con.Close();
                }
                con.Open();
                SqlCommand kmt8 = new SqlCommand("Select KARtarih From KAR Where KARtarih = @kartar", con);
                kmt8.Parameters.AddWithValue("@kartar", DateTime.Today.ToShortDateString());
                SqlDataReader kdr = kmt8.ExecuteReader();                    
                if (kdr.Read())
                {
                    con.Close();
                    con.Open();
                    SqlCommand kmt9 = new SqlCommand("Select KAR From KAR Where kartarih=@kartar", con);
                    kmt9.Parameters.AddWithValue("@kartar", DateTime.Today.ToShortDateString());
                    SqlDataReader kda = kmt9.ExecuteReader();
                    while (kda.Read())
                    {
                        kar = Convert.ToInt32(kda["KAR"]);
                    }
                    kar += urunkar;
                    con.Close();
                    con.Open();
                    SqlCommand kmt10 = new SqlCommand("Update KAR Set KAR =@kar Where KARid=(Select KARid From KAR Where KARtarih=@kartar)", con);
                    kmt10.Parameters.AddWithValue("@kar", kar);
                    kmt10.Parameters.AddWithValue("@kartar", DateTime.Today.ToShortDateString());
                    kmt10.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    con.Close();
                    con.Open();
                    SqlCommand kmt11 = new SqlCommand("Insert into KAR(KAR) Values(@kar)", con);
                    kmt11.Parameters.AddWithValue("@kar", urunkar);
                    kmt11.ExecuteNonQuery();
                    con.Close();
                }                
                tbbarkod.Text = "";
                tburun.Text = "";
                tbfiyat.Text = "";
                tbadet.Text = "";
                tbtoplam.Text = "";
                dt.Clear();
                label7.Text = "GENEL TOPLAM : 0 TL";
                gtop = 0;
                
            }
            if (sat == DialogResult.No)
            {

            }
            
        }
    }
}
