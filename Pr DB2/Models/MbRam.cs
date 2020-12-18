using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class MbRam
    {
        public int? MbId { get; set; }
        public int? RamId { get; set; }

        public virtual Motherboard Mb { get; set; }
        public virtual Ram Ram { get; set; }
    }
}
