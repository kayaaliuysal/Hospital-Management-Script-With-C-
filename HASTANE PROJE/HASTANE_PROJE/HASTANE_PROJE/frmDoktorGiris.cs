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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from tbl_doktorlar where doktor_tc=@p1 and doktor_sifre=@p2",baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr_komut = komut.ExecuteReader();

            if (dr_komut.Read() && mskTC.Text!="" && txtSifre.Text!="")
            {
                FrmDoktorDetay fr = new FrmDoktorDetay();
                fr.Tc2 = mskTC.Text;
                fr.Show();                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen Tekrar Giriniz", "Kaya Hastanesi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglan.baglanti().Close();
        }
    }
}
