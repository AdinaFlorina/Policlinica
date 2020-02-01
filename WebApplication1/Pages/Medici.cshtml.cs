using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;

namespace Policlinica
{
    public class MediciModel : PageModel
    {
        public DataTable Med;
        public void OnGet()
        {

            OracleConnection cn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id=c##policlinica;Password=1234;");
            cn.Open();
            string query = "Select * from Medici";
            OracleDataAdapter da = new OracleDataAdapter(query, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Med = dt;
            cn.Close();
        }
    }
}