using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialDesignThemes;


namespace Emlak
{
    public partial class MusteriEkle : Form

    {
        public MusteriEkle()
        {
            InitializeComponent();
        }
        string adi;
        string yol = "Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
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
            SqlDataAdapter da = new SqlDataAdapter("Select * From ilceler WHERE Sehir_ID="+secilen+"", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "isim";
            comboBox2.ValueMember = "ilce_no";
            comboBox2.DataSource = dt;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void MusteriEkle_Load(object sender, EventArgs e)
        {
            cbbsehir();
            cbbil();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Seçiniz";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string yol = openFileDialog1.FileName;
               pictureBox3.Image = Image.FromFile(yol);
                string gün, ay, yıl, saat, dakika, saniye, saise;
                gün = DateTime.Now.Day.ToString();
                ay = DateTime.Now.Month.ToString();
                yıl = DateTime.Now.Year.ToString();
                saat = DateTime.Now.Hour.ToString();
                dakika = DateTime.Now.Minute.ToString();
                saniye = DateTime.Now.Second.ToString();
                saise = DateTime.Now.Millisecond.ToString();
                adi = gün + ay + yıl + saat + dakika + saniye + saise;
                pictureBox3.Image.Save(Application.StartupPath+@"\Re\"+adi+".jpg");
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Musteriler VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)",sc);
            cmd.Parameters.AddWithValue("@P1",textBox1.Text);
            cmd.Parameters.AddWithValue("@P2",textBox2.Text);
            cmd.Parameters.AddWithValue("@P3",richTextBox1.Text);
            cmd.Parameters.AddWithValue("@P4",Convert.ToInt32(textBox3.Text));
            try
            {
                cmd.Parameters.AddWithValue("@P5", adi);
            }
            catch (Exception)
            {

                MessageBox.Show("Resim Alanını Doldurunuz");
            }
            cmd.Parameters.AddWithValue("@P6",Convert.ToInt32(comboBox1.SelectedValue));
            cmd.Parameters.AddWithValue("@P7", Convert.ToInt32(comboBox2.SelectedValue));
            if (checkBox1.Checked)
            {
                cmd.Parameters.AddWithValue("@P8",1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@P8",0);
            }
            if (cmd.ExecuteNonQuery()==1)
            {
                //MessageBox.Show("Kaydınız Oluşturuldu");      
                Form1 f1 = new Form1();
                f1.bildirim(5000,"Emlak","Kaydınız Tamamlandı",ToolTipIcon.Info);
            }
            else
            {
                MessageBox.Show("Hata");
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbil();
        }
    }
}
