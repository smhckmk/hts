using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class YakinTb
    {
        [Key]
        public int yakinTc { get; set; }
        public string adSoyad { get; set; }
        public string telefon { get; set; }
        public string adres { get; set; }
        public int HastaTbhastaTc { get; set; }
        public int DoktorTbdoktorTc { get; set; }

        public DoktorTb doktorTb { get; set; }
       
    }
}