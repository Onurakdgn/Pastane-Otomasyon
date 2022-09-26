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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection bag = new SqlConnection(@"Data Source=.\SQLExpress;Initial Catalog=pastane;Integrated Security=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            kategori_getir();
            hareketleri_listele();
        }


        void kategori_getir()
        {
            string sql = "select * from kategoriler";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            comboBox1.DataSource = tablo;
            comboBox1.ValueMember = "kategorino";
            comboBox1.DisplayMember = "kategori";
        }
        void hareketleri_listele()
        {
            string sql = "select k.kategori, u.urun, h.adet, h.tarih, ht.tip, u.stok from kategoriler k, urunler u, hareketler h, hareket_tipleri ht where k.kategorino = u.kategorino and h.urunno = u.urunno and h.hareket_tip = ht.tipno";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].HeaderCell.Value = "KATEGORİ";
            dataGridView1.Columns[1].HeaderCell.Value = "ÜRÜN ADI";
        }
        void urun_yukle()
        {
            string sql = "select * from urunler where kategorino='" + comboBox1.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "urun";
            listBox1.ValueMember = "urunno";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kategori = comboBox1.SelectedValue.ToString();
            string sql = "select * from urunler where kategorino='" + kategori + "' order by urun";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            listBox1.DataSource = tablo;
            listBox1.ValueMember = "urunno";
            listBox1.DisplayMember = "urun";
        }

        void fiyat_yukle()
        {
            string sql = "select * from urunler where urunno = '" + listBox1.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            textBox2.Text = dt.Rows[0][4].ToString();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string urunno = listBox1.SelectedValue.ToString();
            string sql = "select * from urunler where urunno='" + urunno + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            if (tablo.Rows.Count > 0)
            {
                textBox2.Text = tablo.Rows[0][4].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string urunno = listBox1.SelectedValue.ToString();
            string adet = textBox1.Text;
            string tarih = DateTime.Now.ToShortDateString();
            string sql = "insert into hareketler (urunno,adet,tarih,hareket_tip) values (@urunnu, '" + adet + "', '" + tarih + "', 1)";
            bag.Open();
            SqlCommand komut = new SqlCommand(sql, bag);
            komut.Parameters.AddWithValue("@urunnu", urunno);
            komut.ExecuteNonQuery();
            bag.Close();
            sql = "update urunler set stok=stok+'" + adet + "' where urunno='" + urunno + "'";
            SqlCommand komutt = new SqlCommand(sql, bag);
            bag.Open();
            komutt.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("İşlem Tamam");
            hareketleri_listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string urunno = listBox1.SelectedValue.ToString();
            string adet = textBox1.Text;
            string tarih = DateTime.Now.ToShortDateString();
            string sql = "insert into hareketler (urunno,adet,tarih,hareket_tip) values ('" + urunno + "','" + adet + "','" + tarih + "',2)";
            bag.Open();
            SqlCommand komut = new SqlCommand(sql, bag);
            komut.ExecuteNonQuery();
            bag.Close();
            sql = "update urunler set stok=stok-'" + adet + "' where urunno='" + urunno + "'";
            SqlCommand komutt = new SqlCommand(sql, bag);
            bag.Open();
            komutt.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("İşlem Tamam");
            hareketleri_listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
