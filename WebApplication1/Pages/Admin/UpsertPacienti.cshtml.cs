using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using Policlinica.Models;

namespace Policlinica
{
    public class UpsertPacientiModel : PageModel
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

                        Pacienti.Add(p);
                    }
                }
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
            }
        }

        public IActionResult OnPost(string id, string nume, string prenume)

        {
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "UPDATE pacienti SET NUME = :nume, PRENUME =:prenume WHERE ID_PACIENTI = :id";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@nume", nume);
                cmd.Parameters.Add("@nume", prenume);
                cmd.Parameters.Add("@id", id);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception )
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertpacienti");
        }
        public IActionResult OnPostDelete(string id)
        {
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = @"DELETE from pacienti WHERE ID_PACIENTI = :id";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@id", id);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception )
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertpacienti");


        }
        [BindProperty]
        public Pacient PacNou { get; set; }
        public IActionResult OnPostNew()
        {
            if (PacNou == null)
            {
                return RedirectToPage("/Error");
            }
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = @"INSERT INTO pacienti (ID_PACIENTI, NUME, PRENUME) VALUES( :id, :nume , :prenume)";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@id", PacNou.ID_PACIENTI);
                cmd.Parameters.Add("@nume", PacNou.NUME);
                cmd.Parameters.Add("@prenume", PacNou.PRENUME);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertpacienti");
        }
    }
}