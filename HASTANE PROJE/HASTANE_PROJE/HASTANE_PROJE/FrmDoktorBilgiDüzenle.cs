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
    public partial class FrmDoktorBilgiDüzenle : Form
    {
        public FrmDoktorBilgiDüzenle()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        public string tc_doktor;

        private void FrmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = tc_doktor;

            SqlCommand komut = new SqlCommand("select * from tbl_doktorlar where doktor_tc='" + mskTC.Text + "'", baglan.baglanti());
            SqlDataReader dr_komut = komut.ExecuteReader();
            while (dr_komut.Read())
            {
                txtAd.Text = dr_komut[1].ToString();
                txtSoyad.Text = dr_komut[2].ToString();
                cmbBrans.Text = dr_komut[3].ToString();
                txtSifre.Text = dr_komut[5].ToString();
            }
            baglan.baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update  tbl_doktorlar set doktor_ad=@p1,doktor_soyad=@p2,doktor_brans=@p3,doktor_sifre=@p5 where doktor_tc=@p4", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", tc_doktor);
            komut.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("KAydınız başarıyla güncellenmiştir", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            baglan.baglanti().Close();
        }
    }
}
