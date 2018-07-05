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
    public partial class IlanEkle : Form
    {
        public IlanEkle()
        {
            InitializeComponent();
        }
        string adi;
        string yol = "Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
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
        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
        }

        private void materialRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;


        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {
                openFileDialog1.Title = "Resim Seçiniz";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string yol = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(yol);
                   

                }
            }
            else if (materialRadioButton2.Checked)
            {
                openFileDialog1.Title = "Resim Seçiniz";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string yol = openFileDialog1.FileName;
                    pictureBox2.Image = Image.FromFile(yol);                  
                   

                }
            }
            else if (materialRadioButton3.Checked)
            {
                openFileDialog1.Title = "Resim Seçiniz";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string yol = openFileDialog1.FileName;
                    pictureBox3.Image = Image.FromFile(yol);
                   
                    

                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string gün, ay, yıl, saat, dakika, saniye, saise;
            gün = DateTime.Now.Day.ToString();
            ay = DateTime.Now.Month.ToString();
            yıl = DateTime.Now.Year.ToString();
            saat = DateTime.Now.Hour.ToString();
            dakika = DateTime.Now.Minute.ToString();
            saniye = DateTime.Now.Second.ToString();
            saise = DateTime.Now.Millisecond.ToString();
            adi = gün + ay + yıl + saat + dakika + saniye + saise;
            pictureBox1.Image.Save(Application.StartupPath + @"\ilan\" + adi+"1"+".jpg");
            pictureBox2.Image.Save(Application.StartupPath + @"\ilan\" + adi +"2"+".jpg");
            pictureBox3.Image.Save(Application.StartupPath + @"\ilan\" + adi +"3"+".jpg");
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO ilanlar VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12)",sc);
            cmd.Parameters.AddWithValue("@P1",comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@P2",comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@P3",adi+"1");
            cmd.Parameters.AddWithValue("@P4",adi+"2");
            cmd.Parameters.AddWithValue("@P5",adi+"3");
            cmd.Parameters.AddWithValue("@P6",comboBox3.SelectedValue);
            cmd.Parameters.AddWithValue("@P7",comboBox4.SelectedValue);
            cmd.Parameters.AddWithValue("@P8",richTextBox1.Text);
            if (materialCheckBox1.Checked)
            {
                cmd.Parameters.AddWithValue("@P9",1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@P9", 0);
            }
            
            cmd.Parameters.AddWithValue("@P10", materialSingleLineTextField1.Text);
            cmd.Parameters.AddWithValue("@P11",richTextBox2.Text);
            cmd.Parameters.AddWithValue("@P12", materialSingleLineTextField2.Text);
            if (cmd.ExecuteNonQuery()==1)
            {
                MessageBox.Show("Başarıyla Kaydedildi");
            }
          
        }

        private void IlanEkle_Load(object sender, EventArgs e)
        {
            cbbilans();
            cbbilansmus();
            cbbildoldur();
            cbbilcedoldur();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbilcedoldur();
        }
    }
}
