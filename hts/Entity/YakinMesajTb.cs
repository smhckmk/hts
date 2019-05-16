using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class YakinMesajTb
    {
        public int Id { get; set; }
        public string mesaj { get; set; }
        public string tarih { get; set; }
        public int YakinTbyakinTc { get; set; }

        public YakinTb yakinTb { get; set; }
    }
}