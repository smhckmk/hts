using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class AcilDurumTb
    {
        public int Id { get; set; }
        public double aKonumX { get; set; }
        public double aKonumY { get; set; }
        public double aSicaklik { get; set; }
        public double aNabiz { get; set; }
        public DateTime olcumZamani { get; set; }
        public int HastaTbhastaTc { get; set; }

        public HastaTb hastaTb { get; set; }
    }
}