using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using Policlinica.Models;

namespace Policlinica
{
    [Authorize(Roles = "Admin")]
    public class UpsertDepartamentModel : PageModel
    {
        public List<Departament> Departamente { get; set; }
        public void OnGet()
        {
            Departamente = new List<Departament>();
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "Select * from Departamente";
                using (OracleDataReader dataReader = new OracleCommand(query, oracleConnection).ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Departament d = new Departament();
                        d.ID_DEPARTAMENTE = (decimal)dataReader["ID_DEPARTAMENTE"];
                        d.DENUMIRE = dataReader["DENUMIRE"].ToString();

                        Departamente.Add(d);
                    }
                }
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
            }
        }

        public IActionResult OnPost(string id, string denumire)

        {
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "UPDATE departamente SET DENUMIRE = :denumire WHERE ID_DEPARTAMENTE = :id";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@denumire", denumire);
                cmd.Parameters.Add("@id", id);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertdepartament");
        }
        public IActionResult OnPostDelete(string id)
        {
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = @"DELETE from departamente WHERE ID_DEPARTAMENTE = :id";
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
            return RedirectToPage("/admin/upsertdepartament");


        }
        [BindProperty]
        public Departament DepNou { get; set; }
        public IActionResult OnPostNew()
        {
            if (DepNou == null)
            {
                return RedirectToPage("/Error");
            }
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = @"INSERT INTO departamente (DENUMIRE) VALUES(:denumire)";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@denumire", DepNou.DENUMIRE);
                cmd.ExecuteNonQuery();
                oracleConnection.Close();
            }
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertdepartament");
        }
    }
}