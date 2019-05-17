using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class DataInitiliazer : DropCreateDatabaseIfModelChanges<htsContext>
    {
        protected override void Seed(htsContext context)
        {

            List<KurumTb> kurum = new List<KurumTb>()
            {
                new KurumTb(){kurumAdi="AHTS", misyon="Etik ilkelerden asla ödün vermeden seçkin kadrosu ile bilimsel ve teknolojik gelişmeleri takip eden; hasta, hasta yakını ve çalışan memnuniyeti odaklı, uluslararası kalite standartlarında hizmet anlayışı ile sektörde öncü uygulamalarla fark yaratarak sağlıkta dünya markası olmak.",
                                               subeler ="Yenimahalle Şubesi" +
                                               "Talas Şubesi" +
                                               "Alparslan Şubesi",
                                               hizmetler ="Hemşirelik hizmetleri hastanenin diğer birimleri ile işbirliği içinde ve ekip çalışması yaklaşımıyla yürütülmelidir. Amaç hasta ve ailesinin gereksinim ve beklentileri doğrultusunda kapsamlı etkili ve iyi planlanmış bir hemşirelik bakımının uygulanmasıdır." +
                                               "Hacettepe Hastanelerinde kaliteli bir hemşirelik hizmetleri için, ‘hemşirelik hizmeti nedir’ açıkça belirlenmiş, hemşirelik hizmetleri personelinin iş analizi yapılmış, iş tanımları belirlenmiş ve hemşirelik hizmetlerinin niteliğini değerlendirme standartları oluşturulmuştur. " +
                                               "Bu bakım standartları tüm hemşirelerin hastanenin her biriminde aynı standartlarda bakım verilmesini sağlamaktadır.",
                                               iletisim ="İletişim Bilgileri " +
                                               "Adres:Hacettepe Mh. 06230 Ankara Türkiye" +
                                               "Telefon: +90(312) 305 50 00" +
                                               "Faks: +90(312) 305 50 00" +
                                               "Email: hastane @hacettepe.edu.tr",
                                               kullaniciAdi ="admin",
                                               sifre ="123",
                                              }


            };
            foreach (var item in kurum)
            {
                context.Kurumlar.Add(item);
            }
            List<UzmanlikTb> uzmanlikTb = new List<UzmanlikTb>()
            {
                new UzmanlikTb()
                {
                    uzmanlikAdi="Kardiyoloji"
                },
                 new UzmanlikTb()
                {
                    uzmanlikAdi="Uroloji"
                },
                  new UzmanlikTb()
                {
                    uzmanlikAdi="KBB"
                }
            };
            foreach (var item in uzmanlikTb)
            {
                context.Uzmanlar.Add(item);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}