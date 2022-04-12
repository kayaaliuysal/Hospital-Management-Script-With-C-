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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        void refresh()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_randevular ", baglan.baglanti());
            DataSet ds = new DataSet();           
            da.Fill(ds, "Tbl_randevular");
            dataGridView1.DataSource = ds.Tables["tbl_randevular"];

        }

        public string tc;

        
        

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
                        
            lblTC.Text = tc;
            
            // Hasta Ad Soyad çekme

            SqlCommand komut = new SqlCommand("select hasta_ad,hasta_soyad from Tbl_Hastalar where hasta_tc=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr_komut = komut.ExecuteReader();
            while (dr_komut.Read())
            {
                lblAd.Text = dr_komut[0].ToString();
                lblSoyad.Text = dr_komut[1].ToString();
            }
            baglan.baglanti().Close();

            // Randevu Geçmişi

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * From tbl_randevular where hasta_TC=" + lblTC.Text, baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
                                                                 
            // Brans Adlarını Database'den Combobox'a çekme

            SqlCommand brans_cekme = new SqlCommand("select * from tbl_branslar", baglan.baglanti());
            SqlDataReader dr_brans_cekme = brans_cekme.ExecuteReader();
            while (dr_brans_cekme.Read())
            {
                cmbBrans.Items.Add(dr_brans_cekme[1]);
            }
            baglan.baglanti().Close();        

                    
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktorAd.Items.Clear();
            SqlCommand doktor_cekme = new SqlCommand("select doktor_ad,doktor_soyad from tbl_doktorlar where doktor_brans=@p1", baglan.baglanti());
            doktor_cekme.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr_doktor_cekme = doktor_cekme.ExecuteReader();
            while (dr_doktor_cekme.Read())
            {
                cmbDoktorAd.Items.Add(dr_doktor_cekme[0]+" "+ dr_doktor_cekme[1]);
                
                
                

                
            }
        }

        private void cmbDoktorAd_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter dr = new SqlDataAdapter("select * from tbl_randevular where randevu_brans='" + cmbBrans.Text + "'"+ "and randevu_doktor='"+cmbDoktorAd.Text + "'and randevu_durum=0", baglan.baglanti());
            dr.Fill(dt);
            dataGridView2.DataSource = dt;
                
        
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDüzenle fr = new FrmHastaBilgiDüzenle();
            fr.tc = lblTC.Text;
            fr.ad = lblAd.Text;
            fr.soyad = lblSoyad.Text;
            fr.Show();
           

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtİd.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();

        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update tbl_randevular set Randevu_durum=1 , hasta_tc=@p1, hasta_sikayet=@p2 where randevu_id=@p3", baglan.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            komut1.Parameters.AddWithValue("@p2", rchSikayet.Text);
            komut1.Parameters.AddWithValue("@p3", txtİd.Text);
            komut1.ExecuteNonQuery();
            baglan.baglanti().Close();
            
            MessageBox.Show("Randevunuz Başarıyla Oluşturulmuştur", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

    }
}
