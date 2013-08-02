using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linda.Demo.Web.Controllers
{
    using Linda.Core;
    using Linda.Core.Lookup;
    using Linda.Demo.Console;

    public class YamlController : Controller
    {
        public ActionResult Index()
        {
            var manager = new DefaultConfigManager();

            var config = manager.GetConfig<Configuration>();

            ViewBag.Message = config.Startup.SupportedRuntime.Sku + "\n" + config.Startup.SupportedRuntime.Version;

            return View();
        }
    }
}
