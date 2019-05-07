using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class HastaneTb
    {
        [Key]     
        public int hastaneId { get; set; }
        public string hastaneAdi { get; set; }
        public string stoktakiBileklik { get; set; }
        public string telefon { get; set; }
        public string adres { get; set; }
        public int KurumTbkurumId { get; set; }


        public KurumTb kurumTb { get; set; }
        public List<DoktorTb> doktorlar { get; set; }
    }
}