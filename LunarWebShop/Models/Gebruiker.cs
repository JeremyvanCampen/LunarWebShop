using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace LunarWebShop.Models
{ 
    public abstract class Gebruiker
    {
        public int GebruikerID;
        [Display(Name = "Voornaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Voornaam moet ingevoerd worden!")]
        public string Voornaam { get; set; }

        [Display(Name = "Achternaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Achternaam moet ingevoerd worden!")]
        public string Achternaam { get; set; }

        [Display(Name = "Gebruikersnaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gebruikersnaam moet ingevoerd worden!")]
        public string Gebruikersnaam { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wachtwoord moet ingevoerd worden!")]
        [DataType(dataType: DataType.Password)]
        [MinLength(6, ErrorMessage = "Wachtwoord moet minimaal 6 karakters lang zijn.")]
        public string Wachtwoord { get; set; }

        [Display(Name = "Bevestig Wachtwoord")]
        [DataType(dataType: DataType.Password)]
        [Compare("Wachtwoord", ErrorMessage = "Beide wachtwoorden zijn niet hetzelfde")]
        public string BevestigWachtwoord { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email moet worden ingevoerd!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Geboortedatum { get; set; }
    }
}
