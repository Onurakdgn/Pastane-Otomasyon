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

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection bag = new SqlConnection(@"Data Source =.\SQLExpress;Initial Catalog = pastane;Integrated Security =True");

        private void button1_Click(object sender, EventArgs e)
        {
            string kuladi = textBox1.Text;
            string sifre = textBox2.Text;
            string sql = "select * from kullanicilar where kuladi='" + kuladi + "' and sifre = '" + sifre + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Form2 frm2 = new Form2();
                frm2.Show();
                Hide();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Kullanıcı adı veya şifre yanlış";
                label3.Font = new Font("Arial", 8, FontStyle.Bold);
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                textBox2.PasswordChar = '\0';
            }

            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
