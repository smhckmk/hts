using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Models
{
    public class YakinHastaAcilDurum
    {
        public IEnumerable<Entity.YakinTb> yakin { get; set; }
        public IEnumerable<Entity.AcilDurumTb> acildurum { get; set; }
        public IEnumerable<Entity.HastaTb> hastalar { get; set; }
    }
}