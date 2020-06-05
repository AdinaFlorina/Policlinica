using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Policlinica.Models
{
    public class Pacient
    {
        [BindProperty]
        [Display(Name = "ID pacienti")]
        [Required]
        public decimal? ID_PACIENTI { get; set; }
        [BindProperty]
        [Display(Name = "Nume")]
        [Required]
        public string NUME { get; set; }
        [BindProperty]
        [Display(Name = "Prenume")]
        [Required]
        public string PRENUME { get; set; }
        public string CNP { get; set; }
        public DateTime DATA_NASTERII { get; set; }
        public string ADRESA { get; set; }
        public string ASIGURARE_MEDICALA { get; set; }
        public decimal? ID_AFECTIUNI { get; set; }
        public decimal? ID_TRATAMENTE { get; set; }
        public decimal? ID_MEDICI { get; set; }
    }
}
