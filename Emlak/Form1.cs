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
using System.IO;

namespace Emlak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string yol = @"Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
        int hak;
        public void bildirim(int sure , string baslık,string icerik,ToolTipIcon ıcon)
        {
            notifyIcon1.ShowBalloonTip(sure,baslık,icerik,ıcon);
        }
        private static void dosyayaYaz(int hak)
        {
            string dosya_yolu = Application.StartupPath+@"\Emlak.t";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);     
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(hak);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string dosya_yolu = Application.StartupPath + @"\Emlak.t";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                MessageBox.Show(yazi + " Hakkınız Kaldı");
                yazi = sw.ReadLine();
                int s = Convert.ToInt32(yazi)-1;
                dosyayaYaz(s);
            }
            sw.Close();
            fs.Close();

           

           
            string gun, ay, yıl,t;
            gun = DateTime.Now.Day.ToString();
            ay = DateTime.Now.Month.ToString();
            yıl = DateTime.Now.Year.ToString();
            t = yıl + "-" + ay +"-"+ gun;
            int sat = DateTime.Now.Day;
            
            
            if (sat<32)
            {


                if (DateTime.Now.Day == sat)
                {
                    SqlConnection sc = new SqlConnection(yol);
                    sc.Open();
                    SqlCommand cmd = new SqlCommand("Select * From Kiracilar WHERE Kira_Baslan='" + t + "'", sc);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    { 
                        
                    string isim = dr["Kir_Adi"].ToString();
                        bildirim(5000, "Kira Ödeme vakti", isim+"  Kira Ödeme vakti", ToolTipIcon.Warning);
                    }
                    sat++;
                }
            }
            else
            {
                sat = 1;
            }
            
           
        }
        
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Musteriler m = new Musteriler();
            m.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MusteriEkle mek = new MusteriEkle();
            mek.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Kiraci kir = new Kiraci();
            kir.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            KirEkle add = new KirEkle();
            add.Show();
            this.Hide();
        }

        private void çıkışşToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gösterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Show();
        }

        private void sahipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MusteriEkle me = new MusteriEkle();
            this.Hide();
            me.Show();
        }

        private void müşteriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KirEkle ke = new KirEkle();
            this.Hide();
            ke.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Ilanlar il = new Ilanlar();
            il.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            IlanEkle ie = new IlanEkle();
            ie.Show();
            this.Hide();
        }

        private void ilanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ilanlar ıl = new Ilanlar();
            ıl.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ödemeler od = new ödemeler();
            od.Show();
            this.Hide();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            ödemeler od = new ödemeler();
            Form1 f1 = new Form1();
            od.Show();
            f1.Hide();
        }
    }
}
