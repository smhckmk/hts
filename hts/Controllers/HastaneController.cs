﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hts.Controllers
{
    public class HastaneController : Controller
    {
        // GET: Hastane
        public ActionResult HastaneAnasayfa()
        {
            return View();
        }
    }
}