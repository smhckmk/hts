using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Models
{
    public class YakinHastaGuncelOlcum
    {
        public IEnumerable<Entity.YakinTb> yakin { get; set; }
        public IEnumerable<Entity.GuncelOlcumTb> guncelOlcum { get; set; }
        public IEnumerable<Entity.HastaTb> hastalar { get; set; }
    }
}