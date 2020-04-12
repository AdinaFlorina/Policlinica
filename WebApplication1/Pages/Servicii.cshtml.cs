using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;

namespace Policlinica
{
    public class ServiciiModel : PageModel
    {
        public DataTable Dep;
        
        public void OnGet()
        {
            OracleConnection cn = new OracleConnection(StaticDetails.ConnectionString.CS);
            cn.Open();
            string query = "Select denumire from Departamente";
            OracleDataAdapter da = new OracleDataAdapter(query, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Dep = dt;
            cn.Close();
        }
    }
}