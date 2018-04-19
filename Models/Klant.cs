using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Klant : Gebruiker

    {
        public int KlantID { get; set; }

        public decimal Saldo { get; set; }

        [Display(Name = "Straatnaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Straatnaam moet ingevoerd worden!")]
        public string Straat { get; set; }

        [Display(Name = "Huisnummer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Huisnummer met ingevoerd worden")]
        public int Huisnummer { get; set; }


    }
}
