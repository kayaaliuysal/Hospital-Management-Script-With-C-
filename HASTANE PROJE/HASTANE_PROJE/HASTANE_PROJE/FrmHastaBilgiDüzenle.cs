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
    public partial class FrmHastaBilgiDüzenle : Form
    {
        public FrmHastaBilgiDüzenle()
        {
            InitializeComponent();
        }

        SqlBaglanti baglan = new SqlBaglanti();

        public string tc;
        public string ad;
        public string soyad;
        
        

        private void FrmHastaBilgiDüzenle_Load(object sender, EventArgs e)
        {
            
            cmbCinsiyet.Items.Add("kadın");

            cmbCinsiyet.Items.Clear();

            mskTC.Text = tc;
            txtAd.Text = ad;
            txtSoyad.Text = soyad;
            SqlCommand komut1 = new SqlCommand("select hasta_telefon,hasta_cinsiyet from tbl_hastalar where hasta_tc=@p1", baglan.baglanti());
            komut1.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr_komut1 = komut1.ExecuteReader();
            while (dr_komut1.Read())
            {
                mskTelno.Text = dr_komut1[0].ToString();
                cmbCinsiyet.Text=dr_komut1[1].ToString();
            }
                       
            
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_hastalar set hasta_ad=@p1, hasta_soyad=@p2, hasta_cinsiyet=@p5, hasta_telefon=@p4,hasta_sifre=@p7 where hasta_tc=@p3  ", baglan.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTC.Text);
            komut2.Parameters.AddWithValue("p4", mskTelno.Text);
            komut2.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p7", txtSifre.Text);
            if (txtAd.Text !="" && txtSoyad.Text !=""  && mskTC.Text !="" && txtSifre.Text !="" && cmbCinsiyet.Text !="" && mskTelno.Text!="")
            {
                
                komut2.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarıyla Güncellenmiştir", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("lütfen gerekli alanları boş bırakmayınız");
            }
            baglan.baglanti().Close();
        }
            
            
        
    }
}
