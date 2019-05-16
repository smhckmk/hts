using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class DataInitiliazer:DropCreateDatabaseIfModelChanges<htsContext>
    {
        protected override void Seed(htsContext context)
        {
            List<KurumTb> kurum = new List<KurumTb>()
            {
                new KurumTb(){kurumAdi="AHTS", misyon="sldnvkldkclkdkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkc",subeler="hhhhhhhhhhhhhhhhhhcbbbbbbbbbbbbbbbbbbbcddbbbbbbbbbbbbbbbbbbbd",hizmetler="jbkcjncdkldmclkdmckmmmmmmmmmmmmmmmmmmklcmdklsmcklsdmlkkkkkkkkkkcdlkmckdmc",iletisim="dhncdkckdsmcldmsşcmdsşmcvlşdmldmvlmdsmvkdmsvklmdskmvkdmvkmdvk",kullaniciAdi="admin",sifre="123",logo="dkncdd"}

            };
            foreach (var item in kurum)
            {
                context.Kurumlar.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}