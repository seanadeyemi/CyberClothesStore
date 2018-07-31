using Store.Domain;
using Store.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.UI.Models
{
    public class CategoryListViewModel
    {
        public IEnumerable<ICategory> Categories { get; set; } = Enumerable.Empty<ICategory>();
    }
}