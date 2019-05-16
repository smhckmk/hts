using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class BileklikTb
    {
        
        public int Id { get; set; }
        public string imalatTarihi { get; set; }
        public Boolean verilebilmeDurumu { get; set; }

        public int? HastaTbhastaTc { get; set; }


        public HastaTb hastaTb { get; set; }

        public BileklikTb()
        {
            verilebilmeDurumu = true;
        }
       
    }
}