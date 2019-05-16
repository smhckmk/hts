using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Models
{
    public class DoktorBileklikHasta
    {
        public IEnumerable<Entity.DoktorTb> doktorlar { get; set; }
        public IEnumerable<Entity.HastaTb> hastalar { get; set; }
        public IEnumerable<Entity.BileklikTb> bileklikler { get; set; }
    }
}