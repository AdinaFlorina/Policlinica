using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPoco;
using Oracle.ManagedDataAccess.Client;
using Policlinica.Models;

namespace Policlinica
{
    [Authorize]
    public class UpsertPacientiModel : PageModel
    {
        public List<Pacient> Pacienti { get; set; }
        public Utilizator Medici { get; set; }
        public List<Utilizator> ListMedici { get; set; }

        [BindProperty]
        public Pacient PacNou { get; set; }

        // Metoda care afiseaza pagina
        public IActionResult OnGet()
        {
            Pacienti = new List<Pacient>();
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "";
                if (HttpContext.User.IsInRole("Medic")) // doar medic
                {
                    //Todo: trebuie sa fac interogare in baza de date ca sa aflu id-ul medicului care este conectat in sistem
                    using (var cn = new OracleConnection(StaticDetails.ConnectionString.CS))
                    {
                        cn.Open();
                        using (var db = new NPoco.Database(cn))
                        {
                            Medici = db.Query<Utilizator>().Where(u => u.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                            //ListMedici = db.Query<Utilizator>().Where(u => u.Rol == "Medic").ToList();
                            //Todo: imi definesc interogarea sql pe baza id-ul aflat in actiunea precenedenta

                            if (Medici == null)
                            {
                                return RedirectToPage("/Error");
                            }
                            query = "Select * from Pacienti where ID_MEDICI=" + Medici.Id;

                        }
                    }
                }
                else //admin sau orice alt rol
                {
                    query = "Select * from Pacienti";

                    using (var cn = new OracleConnection(StaticDetails.ConnectionString.CS))
                    {
                        cn.Open();
                        using (var db = new NPoco.Database(cn))
                        {
                            ListMedici = db.Query<Utilizator>().Where(u => u.Rol == "Medic").ToList();
                        }
                    }
                }
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
            catch (Exception ex)
            {
                oracleConnection.Close();
            }
            return Page();
        }

        // Metoda care actulizeaza datele pacientului
        public IActionResult OnPost(string id, string nume, string prenume)
        {
            OracleConnection oracleConnection = new OracleConnection(StaticDetails.ConnectionString.CS);
            try
            {
                oracleConnection.Open();
                string query = "UPDATE pacienti SET NUME = :nume, PRENUME =:prenume WHERE ID_PACIENTI = :id";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@nume", nume);
                cmd.Parameters.Add("@prenume", prenume);
                cmd.Parameters.Add("@id", id);
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

        // Metoda care sterge pacientul
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
            catch (Exception)
            {
                oracleConnection.Close();
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/admin/upsertpacienti");


        }

        // Metoda care creaza un pacient nou
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
                string query = @"INSERT INTO pacienti (NUME, PRENUME, CNP, DATA_NASTERII, ADRESA, ASIGURARE_MEDICALA, ID_MEDICI) VALUES(:nume , :prenume, :cnp, :datanasterii, :adresa, :asigurare, :idmedic)";
                OracleCommand cmd = new OracleCommand(query, oracleConnection);
                cmd.Parameters.Add("@nume", PacNou.NUME);
                cmd.Parameters.Add("@prenume", PacNou.PRENUME);
                cmd.Parameters.Add("CNP@", PacNou.CNP);
                cmd.Parameters.Add("@datanasterii", PacNou.DATA_NASTERII);
                cmd.Parameters.Add("@adresa", PacNou.ADRESA);
                cmd.Parameters.Add("@asigurare", PacNou.ASIGURARE_MEDICALA);
                cmd.Parameters.Add("@idmedic", PacNou.ID_MEDICI);
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