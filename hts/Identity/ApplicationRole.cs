using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hts.Identity
{
    public class ApplicationRole:IdentityRole

    {
        public string aciklama { get; set; }

        public ApplicationRole()
        {

        }
        public ApplicationRole(string rolename,string aciklama)
        {
            this.aciklama = aciklama;
        }
    }
}