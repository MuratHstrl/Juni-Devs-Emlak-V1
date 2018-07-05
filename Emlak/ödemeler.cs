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
    public partial class ödemeler : Form
    {
        public ödemeler()
        {
            InitializeComponent();
        }
        string yol = @"Data Source=NIRVANA;Initial Catalog=Gercek;Integrated Security=True";
        private void ödemeler_Load(object sender, EventArgs e)
        {
            grid();
        }
        private void grid ()
        {
             string gun, ay, yıl, t;
             gun = DateTime.Now.Day.ToString();
            ay = DateTime.Now.Month.ToString();
            yıl = DateTime.Now.Year.ToString();
            t = yıl + "-" + ay +"-"+ gun;
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Kir_ID as ID,Kir_Adi as Adi,Kir_Soyadi as Soyadi,Kir_Telefon as Numarası,Kir_KayitTarih as GirisTarih,Kira_Baslan as ÖdemeTarihi,Ev_ID as Evi,Fiyat FROM Kiracilar WHERE Kira_Baslan='"+t+"'",sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource=dt;
        }
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            
            SqlConnection sc = new SqlConnection(yol);
            sc.Open();
            SqlCommand cmd =new SqlCommand("UPDATE Kiracilar Set Kira_Baslan=DATEADD(MM,1,Kira_Baslan) Where Kir_ID= "+ dataGridView1.Rows[sec].Cells[0].Value + "", sc);
            if (cmd.ExecuteNonQuery()==1)
            {
                grid();
                MessageBox.Show("Başarılı");
            } 
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sec = Convert.ToInt32(dataGridView1.CurrentRow.Index);
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
