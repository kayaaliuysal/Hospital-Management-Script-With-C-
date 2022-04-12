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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        public string tc;

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            
            // ad soyad Database'den çekme
            SqlCommand komut1 = new SqlCommand("select sekreter_ad,sekreter_soyad from tbl_sekreter where sekreter_tc=@p1",baglan.baglanti());
            komut1.Parameters.AddWithValue("@p1",lblTC.Text );
            SqlDataReader dr_komut1 = komut1.ExecuteReader();
            
            while (dr_komut1.Read())
            {
                lblAd.Text = dr_komut1[0].ToString();
                lblSoyad.Text = dr_komut1[1].ToString();
            }
            baglan.baglanti().Close();

            // Branşları Database'den Datagridview2 ye aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select brans_ad from tbl_branslar", baglan.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            // Doktorların verilerini databaseden datagridview1e aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (doktor_ad+' '+doktor_soyad) as Doktorlar ,doktor_brans from tbl_doktorlar",baglan.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;

            // Branşları Combobox'a çekme

            SqlCommand komut2 = new SqlCommand("select brans_ad from tbl_branslar ", baglan.baglanti());
            SqlDataReader dr_komut2 = komut2.ExecuteReader();
            while (dr_komut2.Read())
            {
                cmbBrans.Items.Add(dr_komut2[0].ToString());
            }

            
                       
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (  mskSaat.Text!="" && mskTarih.Text!="" &&  cmbBrans.Text!="" && cmbDoktor.Text!="")
            {
                SqlCommand kaydet = new SqlCommand("insert into tbl_randevular (randevu_tarih,randevu_saat,randevu_brans,randevu_doktor) values(@p1,@p2,@p3,@p4)", baglan.baglanti());
                kaydet.Parameters.AddWithValue("@p1", mskTarih.Text);
                kaydet.Parameters.AddWithValue("@p2", mskSaat.Text);
                kaydet.Parameters.AddWithValue("@p3", cmbBrans.Text);
                kaydet.Parameters.AddWithValue("@p4", cmbDoktor.Text);
                kaydet.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Randevunuz oluşturulmuştur", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen gerekli alanları doldurunuz", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand doktor_cekme = new SqlCommand("select doktor_ad,doktor_soyad from tbl_doktorlar where doktor_brans=@p1", baglan.baglanti());
            doktor_cekme.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr_doktor_cekme = doktor_cekme.ExecuteReader(); 
            while (dr_doktor_cekme.Read())
            {
                cmbDoktor.Items.Add(dr_doktor_cekme[0]+" "+dr_doktor_cekme[1]);
            }
            baglan.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand duyuru = new SqlCommand("insert into tbl_duyurular (duyuru) values (@p1)", baglan.baglanti());
            duyuru.Parameters.AddWithValue("@p1", richTextBox1.Text);
            duyuru.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturulmuştur", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmSekreterDoktorDüzenle fr = new FrmSekreterDoktorDüzenle();
            fr.Show();

        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmSekreterBransPaneli frm = new FrmSekreterBransPaneli();
            frm.Show();
            
        }

        private void btnRandevuListesi_Click(object sender, EventArgs e)
        {
            FrmSekreterRandevuListe frmRandevu = new FrmSekreterRandevuListe();
            frmRandevu.Show();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update tbl_randevular set (randevu_id=@p1,randevu_tarih=@p2,randevu_saat=@p3,randevu_brans=@p4,randevu_doktor=@p5,randevu_durum=@p6,randevu_tc=@p7)", baglan.baglanti());
            guncelle.Parameters.AddWithValue("@p1", txtİd.Text);
            guncelle.Parameters.AddWithValue("@p2", mskTarih.Text);
            guncelle.Parameters.AddWithValue("@p3", mskSaat.Text);
            guncelle.Parameters.AddWithValue("@p4", cmbBrans.Text);
            guncelle.Parameters.AddWithValue("@p5", cmbDoktor.Text);
            guncelle.Parameters.AddWithValue("@p6", checkBox1.Checked);
            guncelle.Parameters.AddWithValue("@p7", mskTc.Text);
            guncelle.ExecuteNonQuery();
            baglan.baglanti().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDoktorDuyurular frm = new FrmDoktorDuyurular();
            frm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSekreterBilgiDüzenleme fr = new FrmSekreterBilgiDüzenleme();
            fr.tc_sekreter = lblTC.Text;
            fr.Show();
        }
    }
}
