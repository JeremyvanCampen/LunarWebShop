//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LunarWebShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bestelling
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bestelling()
        {
            this.Keycode = new HashSet<Keycode>();
        }
    
        public int BestellingID { get; set; }
        public int AdministratorID { get; set; }
        public int KlantID { get; set; }
    
        public virtual Administrator Administrator { get; set; }
        public virtual Klant Klant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Keycode> Keycode { get; set; }
    }
}
