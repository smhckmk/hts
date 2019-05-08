using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hts.Models
{
    public class KayitOl
    {
        [Required]
        [DisplayName("Kullanici Adi")]
        public string kullaniciAdi { get; set; }

        [Required]
        [DisplayName("Sifre")]
        public string sifre { get; set; }

        [Required]
        [DisplayName("Sifre")]
        [Compare("sifre",ErrorMessage ="sifreniz uyusmuyor.")]
        public string tekrarSifre { get; set; }
    }
}