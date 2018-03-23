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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Keycode = new HashSet<Keycode>();
        }
    
        public int ProductID { get; set; }
        public string Naam { get; set; }
        public Uitgever Uitgever { get; set; }
        public Genre Genre { get; set; }
        public decimal Prijs { get; set; }
        public byte[] Foto { get; set; }
        public Nullable<int> WinkelwagenID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Keycode> Keycode { get; set; }
        public virtual Winkelwagen Winkelwagen { get; set; }
    }
}
