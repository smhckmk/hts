using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class KurumTb
    {
        [Key]
        public int kurumId { get; set; }
        public string kurumAdi { get; set; }       
        public string misyon { get; set; }
        public string subeler { get; set; }
        public string hizmetler { get; set; }
        public string iletisim { get; set; }
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }


        public List<HastaneTb> hastaneler { get; set; }

    }
}