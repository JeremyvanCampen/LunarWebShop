using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    using System;
    using System.Collections.Generic;

    public partial class Keycode
    {
        public int KeycodeID { get; set; }
        public Nullable<int> KlantID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> WinkelwagenID { get; set; }
    }
}
