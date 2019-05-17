using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class DoktorMesajTb
    {
        public int Id { get; set; }
        public string mesaj { get; set; }
        public string tarih { get; set; }

        public int DoktorTbdoktorTc { get; set; }
        public int YakinTbyakinTc { get; set; }


        public DoktorTb doktorTb { get; set; }

       
       
       
    }
}