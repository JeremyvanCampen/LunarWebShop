using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Keycode = new HashSet<Keycode>();
        }
        [Display(Name = "Hoeveelheid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hoeveelheid moet ingevoerd worden")]
        public int Hoeveelheid { get; set; }
        public int ProductID { get; set; }
        [Display(Name = "Naam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Naam moet ingevoerd worden")]
        public string Naam { get; set; }
        public Uitgever Uitgever { get; set; }
        public Genre Genre { get; set; }
        [Display(Name = "Prijs")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Prijs moet ingevoerd worden")]
        public decimal Prijs { get; set; }
        public string Foto { get; set; }
        public string AchtergrondFoto { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase ImageFile2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Keycode> Keycode { get; set; }
    }
}
