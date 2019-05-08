using hts.Identity;
using hts.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hts.Controllers
{
    public class ProfilController : Controller
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public ProfilController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore =new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        // GET: Profil
        public ActionResult KayitOl ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayitOl(KayitOl model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.kullaniciAdi;

                IdentityResult result = UserManager.Create(user, model.sifre);

                if (result.Succeeded)
                {
                    if(RoleManager.RoleExists("doktor"))
                    {
                        UserManager.AddToRole(user.Id, "doktor");
                    }
                    return RedirectToAction("Giris", "Profil");
                   
                }
                else
                {
                    ModelState.AddModelError("error","kullanici Oluşturma Hatası");
                }
            }
            return View(model);
        }
    }
}