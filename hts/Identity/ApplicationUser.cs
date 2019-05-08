using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Identity
{
    public class ApplicationUser:IdentityUser

    {
        public string ad { get; set; }
        public string soyad { get; set; }
    }
}