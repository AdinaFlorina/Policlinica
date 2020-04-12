using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Policlinica.Models
{
    public class Utilizator
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }

        public Utilizator() { }
        public Utilizator(string username, string password, string rol)
        {
            this.UserName = username;
            this.Password = password;
            this.Rol = rol;
        }
    }
}
