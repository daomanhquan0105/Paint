using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPanit.Models;

namespace WebPanit.ViewModel
{
    public class ListProductCategoryViewModel
    {
        public List<ProductCategory> productCategories { get; set; }
        public List<TagProductCategory> tagProductCategories { get; set; }
    }
}