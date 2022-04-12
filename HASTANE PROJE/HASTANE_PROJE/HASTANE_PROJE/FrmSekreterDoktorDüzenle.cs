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
    public partial class FrmSekreterDoktorDüzenle : Form
    {
        public FrmSekreterDoktorDüzenle()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        private void FrmSekreterDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_doktorlar", baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.baglanti().Close();

            // Branşları cmbBox a çekme

            SqlCommand komut2 = new SqlCommand("select brans_ad from tbl_branslar", baglan.baglanti());
            SqlDataReader dr_komut2 = komut2.ExecuteReader();
            while (dr_komut2.Read())
            {
                cmbBrans.Items.Add(dr_komut2[0]);
            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("insert into tbl_doktorlar (doktor_ad,doktor_soyad,doktor_brans,doktor_tc,doktor_sifre) values (@p1,@p2,@p3,@p4,@p5)", baglan.baglanti());
            komut1.Parameters.AddWithValue("@p1", txtAd.Text);
            komut1.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut1.Parameters.AddWithValue("@p4", mskTC.Text);
            komut1.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut1.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Yeni Doktor Kayıdı oluşturulmuştur", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from tbl_doktorlar where doktor_tc=@p1", baglan.baglanti());
            komut2.Parameters.AddWithValue("@p1", mskTC.Text);
            komut2.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Kayıt Silinmiştir", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update tbl_doktorlar set doktor_Ad=@p1,doktor_soyad=@p2,doktor_brans=@p3,doktor_sifre=@p5 where doktor_tc=@p4", baglan.baglanti());
            komut3.Parameters.AddWithValue("@p1", txtAd.Text);
            komut3.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut3.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut3.Parameters.AddWithValue("@p4", mskTC.Text);
            komut3.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut3.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor Kayıdı Güncellenmiştir", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
    }
}
