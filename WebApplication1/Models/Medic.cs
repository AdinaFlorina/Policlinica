using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Policlinica.Models
{
    public class Medic
    {
        [BindProperty]
        [Display(Name = "ID medici")]
        [Required]
        public decimal? ID_MEDICI { get; set; }
        [BindProperty]
        [Display(Name = "Nume")]
        [Required]
        public string NUME { get; set; }
        [BindProperty]
        [Display(Name = "Prenume")]
        [Required]
        public string PRENUME { get; set; }
        public string CNP { get; set; }
        public DateTime DATA_NASTERII  { get; set; }
        public DateTime DATA_ANGAJARII { get; set; }
        public string ADRESA { get; set; }
        public decimal? ID_DEPARTAMENTE { get; set; }

        public int ID_UTILIZATOR { get; set; }

    }
}
