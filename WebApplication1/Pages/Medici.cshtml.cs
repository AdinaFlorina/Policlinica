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

            OracleConnection cn = new OracleConnection(StaticDetails.ConnectionString.CS);
            cn.Open();
            string query = @"SELECT
     medici.nume || ' ' || medici.prenume as ""Numele si prenumele"",
    departamente.denumire as Departament
FROM
    departamente
    INNER JOIN medici ON medici.id_medici = departamente.id_medici";
            OracleDataAdapter da = new OracleDataAdapter(query, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Med = dt;
            cn.Close();
        }
    }
}