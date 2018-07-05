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
    public partial class Musteriler : Form
    {
        public Musteriler()
        {
            InitializeComponent();
        }
        string yol = "Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
        private void cbbildoldur()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Sehir",sc);
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
            SqlDataAdapter da = new SqlDataAdapter("Select * From ilceler WHERE Sehir_ID="+scilen+"", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "isim";
            comboBox2.ValueMember = "ilce_no";
            comboBox2.DataSource = dt;
        }
        private void griddoldur()
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Musteriler", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Musteriler_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode =
        DataGridViewAutoSizeColumnsMode.AllCells;
            cbbilcedoldur();
            cbbildoldur();          
            griddoldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            string Rismi = dataGridView1.Rows[sec].Cells[5].Value.ToString();
            string isim = dataGridView1.Rows[sec].Cells[1].Value.ToString();
            pictureBox3.Image = Image.FromFile(Application.StartupPath+@"\Re\"+Rismi+".jpg");
            label5.Text = isim;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sc = new SqlConnection(yol);
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Musteriler WHERE Musteri_Telefon LIKE '%" + Convert.ToInt32(textBox1.Text) + "%'", sc);
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
                SqlDataAdapter da = new SqlDataAdapter("Select * From Musteriler WHERE Musteri_Sehir=" + secilen + "", sc);
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
                SqlDataAdapter da = new SqlDataAdapter("Select * From Musteriler WHERE Musteri_il=" + secilen + "", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                griddoldur();    
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            griddoldur();
            if(checkBox1.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                SqlConnection sc = new SqlConnection(yol);
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Musteriler WHERE Musteri_Onemi=" + 1 + "", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                griddoldur();
            }
        }
    }
}
