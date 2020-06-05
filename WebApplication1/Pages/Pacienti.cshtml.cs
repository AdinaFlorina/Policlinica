using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using Policlinica.Models;

namespace Policlinica.Pages
{
    [Authorize]
    public class PacientiModel : PageModel
    {
        public DataTable Pacient;
        public void OnGet()
        {

            OracleConnection cn = new OracleConnection(StaticDetails.ConnectionString.CS);
            cn.Open();
            string query = @"SELECT
     pacienti.nume || ' ' || pacienti.prenume as Pacient,
    medici.nume || ' ' || medici.prenume as Medic
FROM
    medici
    INNER JOIN pacienti ON pacienti.ID_PACIENTI = medici.ID_MEDICI";
            OracleDataAdapter da = new OracleDataAdapter(query, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Pacient = dt;
            cn.Close();
        }
    }

}