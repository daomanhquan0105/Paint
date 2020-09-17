using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPanit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProduct()
        {
            return View();
        }

        public ActionResult ProductDetail()
        {
            return View();
        }
        public ActionResult TableColorPaint()
        {
            return View();
        }

        public ActionResult ListPost()
        {
            return View();
        }

        #region Partial
        [ChildActionOnly]
        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCardPartial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult ActiveProductForCategoryPartial()
        {
            return PartialView();
        }
        public PartialViewResult MenuMobilePartial()
        {
            return PartialView();
        }
        public PartialViewResult ListProdutPartial()
        {
            return PartialView();
        }
        public PartialViewResult ProductDetailPartial()
        {
            return PartialView();
        }
        public PartialViewResult MenuXLPartial()
        {
            return PartialView();
        }
        #endregion
    }
}