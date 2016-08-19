using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KapyApp.Models;

namespace KapyApp.Controllers
{
    public class CategoryController : Controller
    {
        kapymvc1Entities db = new kapymvc1Entities();
        // GET: Category
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

            return RedirectToAction("CategoryIndex", "Category");
        }
    }
}