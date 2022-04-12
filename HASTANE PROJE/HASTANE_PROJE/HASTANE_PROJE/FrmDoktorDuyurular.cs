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
    public partial class FrmDoktorDuyurular : Form
    {
        public FrmDoktorDuyurular()
        {
            InitializeComponent();
        }

        SqlBaglanti baglan = new SqlBaglanti();

        private void FrmDoktorDuyurular_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_duyurular",baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.baglanti().Close();
        }
    }
}
