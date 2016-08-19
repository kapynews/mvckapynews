using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KapyApp.Models;
using System.Text;

namespace KapyApp.Controllers
{
    public class UserController : Controller
    {
        kapymvc1Entities db = new kapymvc1Entities();

        // GET: User

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateNewAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewAccount(User user)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (kapymvc1Entities db = new kapymvc1Entities())
                {
                    user.roleId = 2;
                    db.Users.Add(user);
                    db.SaveChanges();
                    ModelState.Clear();
                    
                    //ViewBag.Message = user.userName + "is successfully Registration!";

                    return RedirectToAction("CategoryIndex","Home");
                }
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            using (kapymvc1Entities db = new kapymvc1Entities())
            {
                var usr = db.Users.Single(u => u.userName == user.userName && u.password == user.password);
                if (usr != null)
                {
                    Session["userId"] = usr.userId.ToString();
                    Session["userName"] = usr.userName.ToString();

                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong.");
                } 
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        [HttpGet]
        public ActionResult SelectCategory()
        {
            kapymvc1Entities db = new kapymvc1Entities();
            return View(db.Categories);
        }

        [HttpPost]
        public ActionResult SelectCategory(IEnumerable<Category> categories)
        {





            return RedirectToAction("CategoryIndex", "Categories");
        }

       
       

        //[HttpGet]
        //public ActionResult SelectCategory()
        //{
        //    return View();
        //}


        //[HttpPost]
        // [ValidateAntiForgeryToken]
        //public ActionResult SelectCategory(IEnumerable<Category> categories)
        //public ActionResult SelectCategory(Category categories)
        // {
        // RedirectToAction("SubmitSelected");
        //return View("SelectCategory");
        //return PartialView("SelectCategory1()", db.Categories.ToList());
        //   return View(categories);
        //  }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SubmitSelected(CategoriesSelectionViewModel model)
        //{
        //    // get the ids of the items selected:

        //    var selectedCateIds = model.getSelectedIds();


        //    // Use the ids to retrieve the records for the selected people
        //    // from the database:
        //    var selectedCates = from cate in db.Categories
        //                        where selectedCateIds.Contains(cate.categoryId)
        //                        select cate;

        //    // Process according to your requirements:
        //    foreach (var cate in selectedCates)
        //    {
        //        System.Diagnostics.Debug.WriteLine(
        //            string.Format("{0} {1}", cate.categoryId, cate.categoryName));
        //    }

        //    // Redirect somewhere meaningful (probably to somewhere showing 
        //    // the results of your processing):
        //    return RedirectToAction("UserProfile");
        //}




    }
}