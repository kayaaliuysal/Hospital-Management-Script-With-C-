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
    public partial class FrmSekreterRandevuListe : Form
    {
        public FrmSekreterRandevuListe()
        {
            InitializeComponent();
        }
        SqlBaglanti baglan = new SqlBaglanti();

        public int secilen;

        private void FrmSekreterRandevuListe_Load(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular", baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.baglanti().Close();

        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             secilen = dataGridView1.SelectedCells[0].RowIndex;


        }
    }
}
