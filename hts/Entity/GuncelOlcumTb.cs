using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class GuncelOlcumTb
    {
        public int Id { get; set; }
        public DateTime sonOlcumTarihi { get; set; }
        public double guncelKonumX { get; set; }
        public double guncelKonumY { get; set; }
        public double guncelSicaklik { get; set; }
        public double guncelNabiz { get; set; }
        public int HastaTbhastaTc { get; set; }


        public HastaTb hastaTb { get; set; }
    }
}