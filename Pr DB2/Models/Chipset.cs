using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class Chipset
    {
        public Chipset()
        {
            Motherboards = new HashSet<Motherboard>();
        }

        public int Id { get; set; }
        public bool? SioSup { get; set; }
        public bool? PciSup { get; set; }
        public bool? PcieSup { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
