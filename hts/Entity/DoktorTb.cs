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
        public int? toplamBileklikSayisi { get; set; }
        public int? kontrolundekiBileklikSayisi { get; set; }
        public int? UzmanlikTbId { get; set; }

        public HastaneTb hastaneTb { get; set; }
        public UzmanlikTb uzmanlikTb { get; set; }
        public List<YakinTb> yakinlar { get; set; }
        public List<HastaTb> hastalar { get; set; }
    }
}