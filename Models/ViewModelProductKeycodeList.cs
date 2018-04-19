using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ViewModelProductKeycodeList
    {

        public List<Product> Product { get; set; }

        public decimal TotaalBedrag { get; set; }

        public ViewModelProductKeycodeList()
        {
            Product = new List<Product>();
            TotaalBedrag = 0;
        }
    }
}
