using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Policlinica.Models
{
    public class Departament
    {
        [BindProperty]
        [Display(Name ="ID departament")]
        [Required]
        public decimal? ID_DEPARTAMENTE { get; set; }
        [BindProperty]
        [Display(Name = "Denumire departament")]
        [Required]
        public string  DENUMIRE { get; set; }
    }
}
