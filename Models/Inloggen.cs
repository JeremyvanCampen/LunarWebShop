using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Inloggen
    {
        [Display(Name = "Gebruikersnaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gebruikersnaam moet ingevuld worden.")]
        public string Gebruikersnaam { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wachtwoord moet ingevuld worden")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }

        [Display(Name = "Onthoudt mijn inloggegevens")]
        public bool OnthoudtMijnInloggegevens { get; set; }
    }
}
