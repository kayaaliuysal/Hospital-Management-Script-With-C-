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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        SqlBaglanti baglan = new SqlBaglanti();

        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayıt frHastaKayıt = new FrmHastaKayıt();
            frHastaKayıt.Show();
            
        }

        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand giris = new SqlCommand("select * from Tbl_Hastalar where hasta_tc=@p1 and hasta_sifre=@p2", baglan.baglanti());
            giris.Parameters.AddWithValue("@p1", mskTC.Text);
            giris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr_giris = giris.ExecuteReader();            
            if (dr_giris.Read() && mskTC.Text!="" && txtSifre.Text!="")
            {
                FrmHastaDetay fr = new FrmHastaDetay();
                fr.tc = mskTC.Text;               
                fr.Show();
                this.Hide();
            }
            else
            {
                mskTC.Clear();
                txtSifre.Clear();
                mskTC.Focus();
                MessageBox.Show("lütfen girdiğiniz verileri tekrar kontrol edin", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglan.baglanti().Close();
        
        }

    }
}
