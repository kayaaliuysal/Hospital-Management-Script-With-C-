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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        SqlBaglanti baglan = new SqlBaglanti();

        public string Tc2;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = Tc2;
            // Doktor ad soyad databaseden çekme
            SqlCommand komut = new SqlCommand("select doktor_ad,doktor_soyad from tbl_doktorlar where doktor_tc=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr_komut = komut.ExecuteReader();
            while (dr_komut.Read())
            {
                lblAd.Text = dr_komut[0].ToString();
                lblSoyad.Text = dr_komut[1].ToString();
            }
            baglan.baglanti().Close();

            //Doktora ait olan randevuları görüntüleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where randevu_doktor='"+lblAd.Text+" "+lblSoyad.Text+"'",baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.baglanti().Close();
            
        }

        private void btnBilgiDüzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDüzenle fr = new FrmDoktorBilgiDüzenle();
            fr.tc_doktor = lblTc.Text;
            fr.Show();

        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDoktorDuyurular fr = new FrmDoktorDuyurular();
            fr.Show();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void btnGuvenlik_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GÜVENLİK ÇAĞIRILSIN MI ?.", "Kaya HAstanesi", MessageBoxButtons.YesNoCancel);
            MessageBox.Show("GÜVENLİK ACİL BİR ŞEKİLDE ÇAĞIRILMIŞTIR", "Kaya Hastanesi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
        }
    }
}
