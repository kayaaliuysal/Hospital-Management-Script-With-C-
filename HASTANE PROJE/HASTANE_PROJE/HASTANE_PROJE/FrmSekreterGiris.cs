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

namespace HASTANE_PROJE
{
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        

        private void btnGirisYap_Click(object sender, EventArgs e)
        {

            SqlCommand komut1 = new SqlCommand("select * from tbl_sekreter where sekreter_tc=@p1 and sekreter_sifre=@p2",baglan.baglanti());
            komut1.Parameters.AddWithValue("@p1", mskTC.Text);
            komut1.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr_komut1 = komut1.ExecuteReader();
            
            if (dr_komut1.Read() && mskTC.Text!="" && txtSifre.Text!="")
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();

                fr.tc = mskTC.Text; 
                fr.Show();
                
                this.Hide();
                

            }
            else
            {
                MessageBox.Show("lütfen tekrar kontrol ediniz", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglan.baglanti().Close();
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
