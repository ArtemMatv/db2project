using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class MotherboardsProducer
    {
        public MotherboardsProducer()
        {
            Motherboards = new HashSet<Motherboard>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Con { get; set; }
        public string Fder { get; set; }
        public string Review { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
