using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Policlinica.Models;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Authorization;

namespace Policlinica
{
    [Authorize(Roles = "Admin")]
    public class UpsertMediciModel : PageModel
    {
        public List<Medic> Medici { get; set; }
        public void OnGet()
        {
            Medici = new List<Medic>();
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "Select * from Medici";
                using (OracleDataReader dataReader = new OracleCommand(query, oracleConnection).ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Medic m = new Medic();
                        m.ID_MEDICI = (decimal)dataReader["ID_MEDICI"];
                        m.NUME = dataReader["NUME"].ToString();
                        m.PRENUME = dataReader["PRENUME"].ToString();

                        Medici.Add(m);
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
                string query = "UPDATE medici SET NUME = :nume, PRENUME =:prenume WHERE ID_MEDICI = :id";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@nume", nume);
                cmd.Parameters.Add("@nume", prenume);
                cmd.Parameters.Add("@id", id);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertmedici");
        }
        public IActionResult OnPostDelete(string id)
        {
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = @"DELETE from medici WHERE ID_MEDICI = :id";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@id", id);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertmedici");


        }
        [BindProperty]
        public Medic MedNou { get; set; }
        public IActionResult OnPostNew()
        {
            if (MedNou == null)
            {
                return RedirectToPage("/Error");
            }
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = @"INSERT INTO medici (ID_MEDICI, NUME, PRENUME) VALUES( :id, :nume , :prenume)";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@id", MedNou.ID_MEDICI);
                cmd.Parameters.Add("@nume", MedNou.NUME);
                cmd.Parameters.Add("@prenume", MedNou.PRENUME);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertmedici");
        }
    }
}