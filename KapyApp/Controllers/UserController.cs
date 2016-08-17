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
                    
                    ViewBag.Message = user.userName + "is successfully Registration!";

                    return RedirectToAction("CategoryIndex", "Home");
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
        //public string SelectCategory(IEnumerable<Category> categories)
        //{
        //    if (categories.Count(model => (bool)model.isSelected) == 0)
        //    {
        //        return "You have not selected any Category";
        //    }
        //    else
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("You selected – ");
        //        foreach (Category category in categories)
        //        {
        //            if ((bool)category.isSelected)
        //            {
        //                sb.Append(category.categoryName + ", ");
        //            }
        //        }
        //        sb.Remove(sb.ToString().LastIndexOf(","), 1);
        //        return sb.ToString();
        //    }
        //}

        public ActionResult SelectCategory(IEnumerable<Category> categories)
        {
           

            return View("SelectCategory", "User");
            //return PartialView("SelectCategory1()", db.Categories.ToList());
        }





    }
}