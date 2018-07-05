using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Emlak
{
    public partial class Kiraci : Form
    {
        public Kiraci()
        {
            InitializeComponent();
        }
        string yol = "Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void griddoldur()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kiracilar", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void cbbildoldur()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Sehir", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "Sehir";
            comboBox1.ValueMember = "Sehir_ID";
            comboBox1.DataSource = dt;
        }
        private void cbbilcedoldur()
        {
            int scilen = Convert.ToInt32(comboBox1.SelectedValue);
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From ilceler WHERE Sehir_ID=" + scilen + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "isim";
            comboBox2.ValueMember = "ilce_no";
            comboBox2.DataSource = dt;
        }
        private void Kiraci_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
       DataGridViewAutoSizeColumnsMode.AllCells;
           
            cbbildoldur();
            cbbilcedoldur();
            griddoldur();
            this.dataGridView1.Columns[0].Width = 130;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            string Rismi = dataGridView1.Rows[sec].Cells[7].Value.ToString();
            string isim = dataGridView1.Rows[sec].Cells[1].Value.ToString();
            string Soyisim = dataGridView1.Rows[sec].Cells[2].Value.ToString();
            string Tarih = dataGridView1.Rows[sec].Cells[8].Value.ToString();
            pictureBox3.Image = Image.FromFile(Application.StartupPath + @"\Musteri\" + Rismi + ".jpg");
            label4.Text = isim;
            label5.Text = Soyisim;
            label6.Text = Tarih;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sc = new SqlConnection(yol);
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE Kir_Telefon LIKE '%" + Convert.ToInt32(textBox1.Text) + "%'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                griddoldur();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbilcedoldur();
            try
            {
                int secilen = Convert.ToInt32(comboBox1.SelectedValue);
                SqlConnection sc = new SqlConnection(yol);
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE Kir_Sehir=" + secilen + "", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {

                griddoldur();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int secilen = Convert.ToInt32(comboBox2.SelectedValue);
                SqlConnection sc = new SqlConnection(yol);
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE Kir_ilçe=" + secilen + "", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                griddoldur();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE Kir_Kira=" + 0 + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE Kir_Kira=" + 1 + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE isev=" + 0 + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kiracilar WHERE isev=" + 1 + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
