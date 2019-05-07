using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class htsContext:DbContext
    {
        public htsContext():base("htsConnection")
        {
           
        }
        public DbSet<AcilDurumTb> AcilDurumlar { get; set; }
        public DbSet<BileklikTb> Bileklikler { get; set; }
        public DbSet<DoktorTb> Doktorlar { get; set; }
        public DbSet<GuncelOlcumTb> GuncelOlcumler { get; set; }
        public DbSet<HastaDurumTb> HastaDurumlar { get; set; }
        public DbSet<HastaneTb> Hastaneler { get; set; }
        public DbSet<HastaTb> Hastalar { get; set; }
        public DbSet<KurumTb> Kurumlar { get; set; }
        public DbSet<MuayeneTb> Muayeneler { get; set; }
        public DbSet<YakinTb> Yakinlar { get; set; }
        public DbSet<UzmanlikTb> Uzmanlar { get; set; }

    }
}