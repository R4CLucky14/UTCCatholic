using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTCCatholic.Controllers
{
    public class AwakeningController : Controller
    {
        //
        // GET: /Awakening/
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult About()
		{
			return View();
		}
		public ActionResult Contact()
		{
			return View();
		}
	}
}