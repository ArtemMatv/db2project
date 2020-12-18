using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class SoundChip
    {
        public SoundChip()
        {
            Motherboards = new HashSet<Motherboard>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Infc { get; set; }
        public int? SigNse { get; set; }
        public string Col { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
