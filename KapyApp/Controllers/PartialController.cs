using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KapyApp.Models;

namespace KapyApp.Controllers
{
    public class PartialController : Controller
    {
        private kapymvc1Entities db = new kapymvc1Entities();

        // GET: Parial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryDropList()
        {
            //Linq query to get city details from the DB
            var categoryList = (from p in db.Categories select new { p.categoryId, p.categoryName });

            //Create selectlistitem list 
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem s = null;

            //add the empty selection
            s = new SelectListItem();
            s.Value = "----Select Category----";
            s.Text = "";
            items.Add(s);
            foreach (var t in categoryList)
            {
                s = new SelectListItem();
                s.Text = t.categoryId.ToString();
                s.Value = t.categoryName.ToString();
                items.Add(s);
            }

            //bind seleclistitems list to to viewBag 
            //ViewData["categoryList"] = items;
            ViewBag.categoryList = items;

            //returning the partial view
            return PartialView();
        }

        //public ActionResult _MenuView() {
        //    return PartialView("_MenuView", db.Categories.ToList());
        //}

    }
}