using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HASTANE_PROJE
{
    class SqlBaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglanke = new SqlConnection("Data Source=LAPTOP-91ULT0FF;Initial Catalog=HastaneVeriTabanı;Integrated Security=True");
            baglanke.Open();
            return baglanke;
        }

       

    }
}
