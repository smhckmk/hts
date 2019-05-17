using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class DoktorTb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int doktorTc { get; set; }
        public string adSoyad { get; set; }
        public int? telefon { get; set; }
        public string adres { get; set; }
        public int? maas { get; set; }
        public string mail { get; set; }
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }

        public int? UzmanlikTbId { get; set; }
       
        public List<PuanlamaTb> puanlar { get; set; }

        public HastaneTb hastaneler{ get; set; }

        public UzmanlikTb uzmanlikTb { get; set; }
        public List<YakinTb> yakinlar { get; set; }
        public List<HastaTb> hastalar { get; set; }
        public List<YorumTb> yorumlar { get; set; }



    }
}