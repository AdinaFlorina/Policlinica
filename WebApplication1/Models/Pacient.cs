using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Policlinica.Models
{
    public class Pacient
    {
        public decimal? ID_PACIENTI { get; set; }
        public string NUME { get; set; }
        public string PRENUME { get; set; }
        public string CNP { get; set; }
        public DateTime DATA_NASTERII { get; set; }
        public string ADRESA { get; set; }
        public string ASIGURARE_MEDICALA { get; set; }
        public decimal? ID_AFECTIUNI { get; set; }
        public decimal? ID_TRATAMENTE { get; set; }
    }
}
