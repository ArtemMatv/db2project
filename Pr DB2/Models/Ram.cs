using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class Ram
    {
        public int Id { get; set; }
        public int? Gen { get; set; }
        public string Name { get; set; }
        public int? Fq { get; set; }
        public int? BusFq { get; set; }
        public int? Spd { get; set; }
        public string ModName { get; set; }
    }
}
