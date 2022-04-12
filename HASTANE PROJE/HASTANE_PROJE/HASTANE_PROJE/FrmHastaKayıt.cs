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
    public partial class FrmHastaKayıt : Form
    {
        public FrmHastaKayıt()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        
        
        
        private void btnKayıtyap_Click(object sender, EventArgs e)
        {
            
            SqlCommand kaydet = new SqlCommand("insert into Tbl_Hastalar(hasta_ad,hasta_soyad,hasta_tc,hasta_telefon,hasta_sifre,hasta_cinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglan.baglanti());
            kaydet.Parameters.AddWithValue("@p1", txtAd.Text);
            kaydet.Parameters.AddWithValue("@p2", txtSoyad.Text);
            kaydet.Parameters.AddWithValue("@p3", mskTC.Text);
            kaydet.Parameters.AddWithValue("@p4", mskTelno.Text);
            kaydet.Parameters.AddWithValue("@p5", txtSifre.Text);
            kaydet.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            kaydet.ExecuteNonQuery();
            
            
            
            baglan.baglanti().Close();
            MessageBox.Show("kaydınız gerçekleşmiştir Şifreniz: " + txtSifre.Text, "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmHastaKayıt_Load(object sender, EventArgs e)
        {
            cmbCinsiyet.Items.Add("kadın");
            cmbCinsiyet.Items.Add("erkek");
        }
    }
}
