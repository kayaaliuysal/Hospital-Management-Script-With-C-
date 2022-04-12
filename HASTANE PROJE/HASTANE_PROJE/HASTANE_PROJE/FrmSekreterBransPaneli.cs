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
    public partial class FrmSekreterBransPaneli : Form
    {
        public FrmSekreterBransPaneli()
        {
            InitializeComponent();
        }

        SqlBaglanti baglan = new SqlBaglanti();



        void refresh()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar", baglan.baglanti());
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_branslar");
            dataGridView1.DataSource = ds.Tables["tbl_branslar"];
        }


        private void FrmSekreterBransPaneli_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar", baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.baglanti().Close();



        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand ekle = new SqlCommand("insert into tbl_branslar (brans_ad) values (@p1)", baglan.baglanti());
            ekle.Parameters.AddWithValue("@p1", txtBransAd.Text);            
            ekle.ExecuteNonQuery();
            baglan.baglanti().Close();
            refresh();
            baglan.baglanti().Close();
            MessageBox.Show("Branşınız başarıyla kaydedilmiştir", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtBransId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_branslar where brans_id=@b1", baglan.baglanti());
            komut.Parameters.AddWithValue("@b1",txtBransId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();

            // Burası formu kapamadan Datagridview un refresh atmasını sağlıyor yukarda metoda alıp yapılmış hali var(refresh)
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar",baglan.baglanti());
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_branslar");
            dataGridView1.DataSource = ds.Tables["tbl_branslar"];
            baglan.baglanti().Close();

            MessageBox.Show("Başarıyla Silinmiştir", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update tbl_branslar set brans_ad=@p2 where brans_id=@p1 ", baglan.baglanti());
            guncelle.Parameters.AddWithValue("@p1", txtBransId.Text);
            guncelle.Parameters.AddWithValue("@p2", txtBransAd.Text);
            guncelle.ExecuteNonQuery();
            baglan.baglanti().Close();
            refresh();
            baglan.baglanti().Close();
            MessageBox.Show("Başarıyla Güncellenmiştir","Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
