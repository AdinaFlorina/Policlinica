using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Oracle.ManagedDataAccess.Client;
using Policlinica.Models;

namespace Policlinica.Pages.Admin
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return Page();
        }

        //A very simplistic user store. This would normally be a database or similar.
    //    public List<Utilizator> Users => new List<Utilizator>() {
    //    new Utilizator { UserName = "andrei", Password = "vespa" },
    //    new Utilizator{ UserName = "adina", Password = "12345" }
    //};

        [BindProperty]
        public Utilizator user { get; set; }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            const string badUserNameOrPasswordMessage = "Username or password is incorrect.";
            if (user == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }
            //var lookupUser = Users.FirstOrDefault(u => u.UserName == user.UserName);

            Utilizator lookupUser;
            using (var cn = new OracleConnection(StaticDetails.ConnectionString.CS))
            {
                cn.Open();
                using (var db = new NPoco.Database(cn))
                {
                    lookupUser = await db.QueryAsync<Utilizator>().Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
                }
            }

            if (lookupUser == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            if (lookupUser?.Password != user.Password)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, lookupUser.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, lookupUser.Rol));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (returnUrl == null)
            {
                returnUrl = TempData["returnUrl"]?.ToString();
            }

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("/Index");
        }
    }
}