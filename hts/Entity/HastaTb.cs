using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class HastaTb
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int hastaTc { get; set; }

        public string adSoyad { get; set; }
        public int telefon { get; set; }
        public string adres { get; set; }
        public string hastaligi { get; set; }
        public int DoktorTbdoktorTc { get; set; }



       
       
        
        public DoktorTb doktorTb { get; set; }
        
        public List<GuncelOlcumTb> guncelOlcumler { get; set; }
        public List<AcilDurumTb> acilDurumlar { get; set; }

    }
}