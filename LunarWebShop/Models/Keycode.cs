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
    
    public partial class Keycode
    {
        public int KeycodeID { get; set; }
        public int KlantID { get; set; }
        public int ProductID { get; set; }
        public int BestellingID { get; set; }
    
        public virtual Bestelling Bestelling { get; set; }
        public virtual Klant Klant { get; set; }
        public virtual Product Product { get; set; }
    }
}
