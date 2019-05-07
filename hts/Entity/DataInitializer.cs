using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace hts.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<htsContext>
    {
        protected override void Seed(htsContext context)
        {
            var doktorlar = new List<DoktorTb>()
            {
                new DoktorTb(){doktorTc=1241414,adSoyad="semih",telefon=65364,adres="ıgherg",maas=355,toplamBileklikSayisi=12,kontrolundekiBileklikSayisi=3}
            };
            foreach(var doktor in doktorlar)
            {
                context.Doktorlar.Add(doktor);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }//jkcvbklfdsmvlşmfdv
}