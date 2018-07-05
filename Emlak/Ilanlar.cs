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

namespace Emlak
{
    public partial class Ilanlar : Form
    {
        public Ilanlar()
        {
            InitializeComponent();
        }
        string yol = @"Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
        private void grid()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ilanlar ",sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void cbbilans()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Musteriler ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "Musteri_AdSoyad";
            comboBox1.ValueMember = "Musteri_ID";
            comboBox1.DataSource = dt;
        }
        private void cbbilansmus()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kiracilar ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "Kir_Adi";
            comboBox2.ValueMember = "Kir_ID";
            comboBox2.DataSource = dt;
        }
        private void cbbildoldur()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Sehir", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox3.DisplayMember = "Sehir";
            comboBox3.ValueMember = "Sehir_ID";
            comboBox3.DataSource = dt;
        }
        private void cbbilcedoldur()
        {
            int scilen = Convert.ToInt32(comboBox3.SelectedValue);
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From ilceler WHERE Sehir_ID=" + scilen + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox4.DisplayMember = "isim";
            comboBox4.ValueMember = "ilce_no";
            comboBox4.DataSource = dt;
        }
        private void Ilanlar_Load(object sender, EventArgs e)
        {
            grid();
            cbbilans();
            cbbilansmus();
            cbbildoldur();
            cbbilcedoldur();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
          
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbilcedoldur();
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ilanlar WHERE ilan_sehir="+comboBox3.SelectedValue+"", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ilanlar WHERE ilan_sehir=" + comboBox4.SelectedValue + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (materialCheckBox1.Checked)
                {
                    SqlConnection sc = new SqlConnection(yol);
                    sc.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ilanlar WHERE ilan_kiralik=" + 1 + "", sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception)
            {

                grid();
            }
           
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            grid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            string Rismi = dataGridView1.Rows[sec].Cells[3].Value.ToString();
            string isim = dataGridView1.Rows[sec].Cells[4].Value.ToString();
            string isims = dataGridView1.Rows[sec].Cells[5].Value.ToString();
            if (dataGridView1.Rows[sec].Cells[3].Value.ToString()==null)
            {

            }
            else
            {
                pictureBox3.Image = Image.FromFile(Application.StartupPath + @"\ilan\" + Rismi + ".jpg");
            }
            pictureBox4.Image = Image.FromFile(Application.StartupPath + @"\ilan\" + isim + ".jpg");
            pictureBox5.Image = Image.FromFile(Application.StartupPath + @"\ilan\" + isims + ".jpg");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            if (dataGridView1.Rows[sec].Cells[4].Value.ToString()==null)
            {

                pictureBox4.Visible =false;
            }
            else
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            if (dataGridView1.Rows[sec].Cells[5].Value.ToString() == null)
            {

                pictureBox5.Visible = false;
            }
            else
            {
                pictureBox4.Visible = false;
                pictureBox5.Visible = true;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            if (dataGridView1.Rows[sec].Cells[3].Value.ToString() == null)
            {

                pictureBox3.Visible = false;
            }
            else
            {
                pictureBox5.Visible = false;
                pictureBox3.Visible = true;
            }
        }
    }
}
