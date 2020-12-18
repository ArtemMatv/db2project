using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class Socket
    {
        public Socket()
        {
            Motherboards = new HashSet<Motherboard>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContType { get; set; }
        public int? ContAmo { get; set; }
        public bool? IntGrph { get; set; }
        public string Orien { get; set; }

        public virtual ICollection<Motherboard> Motherboards { get; set; }
    }
}
