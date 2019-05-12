using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class AcilDurumTb
    {
        public int Id { get; set; }
        public string aKonumX { get; set; }
        public string aKonumY { get; set; }
        public double aSicaklik { get; set; }
        public double aNabiz { get; set; }
        public string olcumZamani { get; set; }
        public int HastaTbhastaTc { get; set; }

        public HastaTb hastaTb { get; set; }
    }
}