using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KapyApp.Models
{
    public class CategoriesSelectionViewModel
    {

        public List<SelectCategoryEditorViewModel> Categories { get; set; }
        public CategoriesSelectionViewModel()
        {
            this.Categories = new List<SelectCategoryEditorViewModel>();
        }

        public IEnumerable<int> getSelectedIds()
        {
            return (from c in this.Categories where c.Selected select c.cateId).ToList();
        }
    }
}




