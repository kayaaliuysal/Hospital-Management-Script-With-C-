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
    public partial class FrmSekreterBilgiDüzenleme : Form
    {
        public FrmSekreterBilgiDüzenleme()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        public string tc_sekreter;
        private void FrmSekreterBilgiDüzenleme_Load(object sender, EventArgs e)
        {
            mskTC.Text = tc_sekreter;

            SqlCommand komut = new SqlCommand("select * from tbl_sekreter where sekreter_tc='"+mskTC.Text+"'", baglan.baglanti());
            SqlDataReader dr_komut = komut.ExecuteReader();
            while (dr_komut.Read())
            {
                txtAd.Text = dr_komut[1].ToString();
                txtSoyad.Text = dr_komut[2].ToString();
                txtSifre.Text = dr_komut[4].ToString();
            }
            baglan.baglanti().Close();
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_sekreter set sekreter_ad=@p1,sekreter_soyad=@p2,sekreter_sifre=@p4 where sekreter_tc=@p3", baglan.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTC.Text);
            komut2.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut2.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Kaydınız Başarıyla oluşturulmuştur", "Kaya hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
