using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class YorumTb
    {
        public int Id { get; set; }
        public string yorum { get; set; }
        public int DoktorTbdoktorTc { get; set; }
        

        public DoktorTb doktorTb { get; set; }
        
    }
}