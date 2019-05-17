using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class PuanlamaTb
    {
        public int Id { get; set; }
        public double puan { get; set; }
        public int DoktorTbDoktorTc { get; set; }

        
        public DoktorTb doktorTc { get; set; }
        
    }
}