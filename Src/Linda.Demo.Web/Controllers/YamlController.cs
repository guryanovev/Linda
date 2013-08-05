namespace Linda.Demo.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Linda.Core;
    using Linda.Core.Lookup;
    using Linda.Demo.Web.Models;

    public class YamlController : Controller
    {
        public ActionResult Index()
        {
            var config = new DefaultConfigManager().GetConfig<LindaPage>();

            ViewBag.Title = config.Title;
            ViewBag.LinkToGithub = config.LinkToGithub;
            ViewBag.Tabs = config.Tabs;

            var configDirectory = new DefaultConfigManager(new DirectoryBasedConfigLookup()).GetConfig<LindaPage>();
            config.Tabs.AddRange(configDirectory.Tabs);

            ViewBag.Tabs = config.Tabs;

            return View();
        }
    }
}
