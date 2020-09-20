using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPanit.Models;

namespace WebPanit.ViewModel
{
    public class ProductViewModel
    {
        public PagedList.IPagedList<Product> Products { get; set; }
        public TradeMark TradeMark { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}