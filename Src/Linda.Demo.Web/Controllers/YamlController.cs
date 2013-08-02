using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linda.Demo.Web.Controllers
{
    using Linda.Core;
    using Linda.Demo.Console;
    using Linda.Demo.Web.Models;

    public class YamlController : Controller
    {
        public ActionResult Index()
        {
            var manager = new DefaultConfigManager();

            var config = manager.GetConfig<LindaPage>();

            ViewBag.Title = config.Title;
            ViewBag.LinkToGithub = config.LinkToGithub;
            ViewBag.Tabs = config.Tabs;

            return View();
        }
    }
}
