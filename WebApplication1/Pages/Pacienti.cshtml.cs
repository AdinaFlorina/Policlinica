using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using Policlinica.Models;

namespace Policlinica.Pages
{
    public class PacientiModel : PageModel
    {
        public List<Pacient> Pacienti { get; set; }
        public void OnGet()
        {
            Pacienti = new List<Pacient>();
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "Select * from Pacienti";
                using (OracleDataReader dataReader = new OracleCommand(query, oracleConnection).ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Pacient p = new Pacient();
                        p.ID_PACIENTI = (decimal)dataReader["ID_PACIENTI"];
                        p.NUME = dataReader["NUME"].ToString();
                        p.PRENUME = dataReader["PRENUME"].ToString();
                        p.CNP = dataReader["CNP"].ToString();
                        p.DATA_NASTERII = (DateTime)dataReader["DATA_NASTERII"];
                        p.ADRESA = dataReader["ADRESA"].ToString();
                        p.ASIGURARE_MEDICALA = dataReader["ASIGURARE_MEDICALA"].ToString();
                        p.ID_AFECTIUNI = (decimal)dataReader["ID_AFECTIUNI"];
                        p.ID_TRATAMENTE = (decimal)dataReader["ID_TRATAMENTE"];

                        Pacienti.Add(p);
                    }
                }
                oracleConnection.Close();
            }
            catch (Exception ex)
            {
                oracleConnection.Close();
            }
        }
    }
}