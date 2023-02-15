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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }
        public static string bagla = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = db_market; Integrated Security = True";
        SqlConnection con = new SqlConnection(bagla);
        public static string kasaadi;
        private void button1_Click(object sender, EventArgs e)
        {
            kasaadi = textBox1.Text;
            if (kasaadi=="")
            {
                MessageBox.Show("Kasa adı boş bırakılamaz!");
            }
            else
            {
                con.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM KASA where KASAadi = @n and KASAsifre =@m", con);
                komut.Parameters.AddWithValue("@n", kasaadi);
                komut.Parameters.AddWithValue("@m", textBox2.Text);
                SqlDataReader a = komut.ExecuteReader();
                Boolean b = a.Read();
                con.Close();
                if (b == true)
                {
                    this.Hide();
                    menu go = new menu();
                    go.Show();

                }
                else
                {
                    MessageBox.Show("Yanlış Kasa Adı veya Şifre!");

                }
            }
        }
    }
    
}
