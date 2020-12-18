using System;
using System.Collections.Generic;


namespace Cursova.Models
{
    public partial class Motherboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Series { get; set; }
        public int? MbPrId { get; set; }
        public int? SchId { get; set; }
        public int? ChsId { get; set; }
        public int? SckId { get; set; }
        public string Con { get; set; }
        public bool? IntVid { get; set; }
        public int? RamMax { get; set; }
        public int? RamAmo { get; set; }
        public int? M2Amo { get; set; }
        public int? SataAmo { get; set; }
        public int? PcieAmo { get; set; }
        public string FmbVidOut { get; set; }
        public string FormF { get; set; }
        public double? Price { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }

        public virtual Chipset Chs { get; set; }
        public virtual MotherboardsProducer MbPr { get; set; }
        public virtual SoundChip Sch { get; set; }
        public virtual Socket Sck { get; set; }

    }

    public class MotherboardView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Series { get; set; }
        public string MbPr { get; set; }
        public string Sck { get; set; }
        public string Chs { get; set; }
        public string Sch { get; set; }

        public List<string> Ram { get; set; }
    }

    public class MotherboardDetail
    {
        public string Con { get; set; }
        public bool? IntVid { get; set; }
        public int? RamMax { get; set; }
        public int? RamAmo { get; set; }
        public int? M2Amo { get; set; }
        public int? SataAmo { get; set; }
        public int? PcieAmo { get; set; }
        public string FmbVidOut { get; set; }
        public string FormF { get; set; }
        public double? Price { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
    }
}
