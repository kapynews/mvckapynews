using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KapyApp.Models;
using System.Text;

namespace KapyApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult CategoryIndex()
        {
            kapymvc1Entities db = new kapymvc1Entities();
            return View(db.Categories);
        }

        [HttpPost]
        public string CategoryIndex(IEnumerable<Category> cities)
        {
            if (cities.Count(x => (bool) x.isSelected) == 0)
            {
                return "You have not selected any Category";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You selected – ");
                foreach (Category city in cities)
                {
                    if (city.isSelected)
                    {
                        sb.Append(city.categoryName + ", ");
                    }
                }
                sb.Remove(sb.ToString().LastIndexOf(","), 1);
                return sb.ToString();
            }
        }
    }
}