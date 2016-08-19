using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KapyApp.Models;
using System.Text;

namespace KapyApp.Controllers
{
    public class CategoriesController : Controller
    {
        private kapymvc1Entities db = new kapymvc1Entities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryId,categoryName,isSelected")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoryId,categoryName,isSelected")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //[HttpGet]
        //public ActionResult CategoryIndex()
        //{
        //    kapymvc1Entities db = new kapymvc1Entities();
        //    return View(db.Categories);
        //}

      
        public ActionResult CategoryIndex()
        {
            var model = new CategoriesSelectionViewModel();
            foreach (var category in db.Categories)
            {
                var editorViewModel = new SelectCategoryEditorViewModel()
                {
                    cateId = category.categoryId,
                    cateName = string.Format("{0}, {1}", category.categoryName, category.categoryId),
                    Selected = true

                };
                model.Categories.Add(editorViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult SubmitSelected(CategoriesSelectionViewModel model)
        {
            //get ids from the items selected
            var selectedIds = model.getSelectedIds();

            //use the id to retrieve the record 
            //from the selected categories from te database
            var selectedCategories = from c in db.Categories
                                     where selectedIds.Contains(c.categoryId)
                                     select c;

            foreach (var cate in selectedCategories)
            {
                System.Diagnostics.Debug.WriteLine(
                    string.Format("{0} {1}", cate.categoryId, cate.categoryName)
                    );
            }

            return RedirectToAction("CategoryIndex", "Categories");
        }




        //[HttpPost]
        //public string CategoryIndex(IEnumerable<Category> categories)
        //{
        //    if (categories.Count(x => (bool)x.isSelected) == 0)
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
        //        sb.Remove(sb.ToString().LastIndexOf(','), 1);
        //        return sb.ToString();
        //    }
        //}



    }
}
