using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class UzmanlikTb
    {
        public int Id { get; set; }
        public string uzmanlikAdi { get; set; }

        public List<DoktorTb> doktorlar { get; set; }
    }
}