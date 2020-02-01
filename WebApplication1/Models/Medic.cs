using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Policlinica.Models
{
    public class Medic
    {
        public decimal? ID_MEDICI { get; set; }
        public string NUME { get; set; }
        public string PRENUME { get; set; }
        public string CNP { get; set; }
        public DateTime DATA_NASTERII  { get; set; }
        public DateTime DATA_ANGAJARII { get; set; }
        public string ADRESA { get; set; }
        public decimal? ID_PACIENTI { get; set; }

    }
}
