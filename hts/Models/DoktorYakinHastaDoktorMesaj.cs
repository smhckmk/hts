using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Models
{
    public class DoktorYakinHastaDoktorMesaj
    {
        public IEnumerable<Entity.DoktorTb> doktorlar { get; set; }
        public IEnumerable<Entity.HastaTb> hastalar { get; set; }
        public IEnumerable<Entity.YakinTb> yakinlar { get; set; }
        public IEnumerable<Entity.DoktorMesajTb> mesajlar { get; set; }
    }
}