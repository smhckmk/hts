using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Models
{
    public class Giris
    {
        [Required]
        [DisplayName("Kullanici Adi")]
        public string kullaniciAdi { get; set; }

        [Required]
        [DisplayName("Sifre")]
        public string sifre { get; set; }

        [DisplayName("Beni Hatırla")]
        public bool beniHatirla { get; set; }

    }
}