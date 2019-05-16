using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class MuayeneTb
    {

        public int Id { get; set; }
        public double konumMaxX { get; set; }
        public double konumMaxY { get; set; }
        public double nabizMax { get; set; }
        public double nabizMin { get; set; }
       
        public int HastaTbhastaTc { get; set; }


        public HastaTb hastaTb { get; set; }
    }
}