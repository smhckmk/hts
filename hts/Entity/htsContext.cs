using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class htsContext : DbContext
    {
        public htsContext() : base("htsConnection")
        {
            Database.SetInitializer(new DataInitiliazer());

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
        public DbSet<PuanlamaTb> Puanlar { get; set; }
        public DbSet<DoktorMesajTb> DoktorMesaj { get; set; }
        public DbSet<YakinMesajTb> YakinMesaj { get; set; }
        public DbSet<YorumTb> Yorumlar { get; set; }

    }
}