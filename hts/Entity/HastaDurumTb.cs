using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class HastaDurumTb
    {
        public int Id { get; set; }
        public int yas { get; set; }
        public int kilo { get; set; }
        public int boy { get; set; }
        public double yagOrani { get; set; }
        public double nabiz { get; set; }
        public double? konumY { get; set; }
        public double? konumX { get; set; }
        public string ozelHastaliklar { get; set; }
        public string alerjikDurumlar { get; set; }
        public double kanSekeri { get; set; }

        
        public int HastaTbhastaTc { get; set; }

        
        public HastaTb hastaTb { get; set; }
    }
}