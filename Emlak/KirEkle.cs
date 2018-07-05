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
    public partial class KirEkle : Form
    {
        public KirEkle()
        {
            InitializeComponent();
        }
        string adi;
        string tarih;
        Form1 f1 = new Form1();
        string yol = @"Data Source=NIRVANA;Initial Catalog = Gercek; Integrated Security = True";
        
        private void cbbsehir()
        {
            SqlConnection sc = new SqlConnection(yol);
            SqlDataAdapter da = new SqlDataAdapter("Select * From Sehir", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "Sehir";
            comboBox1.ValueMember = "Sehir_ID";
            comboBox1.DataSource = dt;
        }
        private void cbbil()
        {
            int secilen = Convert.ToInt32(comboBox1.SelectedValue);
            SqlConnection sc = new SqlConnection(yol);
            SqlDataAdapter da = new SqlDataAdapter("Select * From ilceler WHERE Sehir_ID=" + secilen + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "isim";
            comboBox2.ValueMember = "ilce_no";
            comboBox2.DataSource = dt;
        }
        private void cbbev()
        {
            SqlConnection sc = new SqlConnection(yol);
            SqlDataAdapter da = new SqlDataAdapter("Select * From ilanlar", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox3.DisplayMember = "Ilan_Adı";
            comboBox3.ValueMember = "ilan_ID";
            comboBox3.DataSource = dt;
        }
              
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
              openFileDialog1.Title = "Resim Seçiniz";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string yol = openFileDialog1.FileName;
                pictureBox3.Image = Image.FromFile(yol);

            }
        }

        private void KirEkle_Load(object sender, EventArgs e)
        {
            
            cbbil();
            cbbsehir();
            cbbev();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbil();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
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
            tarih = gün + "." + ay + "." + yıl;
            pictureBox3.Image.Save(Application.StartupPath + @"\Musteri\" + adi + ".jpg");
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Kiracilar VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14)",sc);
            cmd.Parameters.AddWithValue("@P1",materialSingleLineTextField1.Text);
            cmd.Parameters.AddWithValue("@P2",materialSingleLineTextField2.Text);
            cmd.Parameters.AddWithValue("@P3",Convert.ToInt32(materialSingleLineTextField3.Text));
            cmd.Parameters.AddWithValue("@P4",materialSingleLineTextField5.Text);
            cmd.Parameters.AddWithValue("@P5",comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@P6",comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@P7",adi);
            cmd.Parameters.AddWithValue("@P8",tarih);
            cmd.Parameters.AddWithValue("@P9",richTextBox1.Text);
            if (materialCheckBox1.Checked)
            {
                cmd.Parameters.AddWithValue("@P10",1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@P10", 0);
            }
            if (materialCheckBox2.Checked)
            {
                cmd.Parameters.AddWithValue("@P11",1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@P11",0);
            }
            cmd.Parameters.AddWithValue("@P12",dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@P13",comboBox3.SelectedValue);
            cmd.Parameters.AddWithValue("@P14",materialSingleLineTextField4.Text);
            if (cmd.ExecuteNonQuery()==1)
            {
                
                f1.bildirim(5000, "Emlak", "Müşteri Kaydınız Tamamlandı", ToolTipIcon.Info);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void materialLabel12_Click(object sender, EventArgs e)
        {

        }
    }
}
