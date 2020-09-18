using Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebPanit.Models;
using WebPanit.ViewModel; 

namespace WebPanit.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataEntities _db = new DataEntities();
        public ActionResult Index()
        {
            return View();
        }
        #region Image Processing Function
        private string AddImageToFolder(HttpPostedFileBase picture, string imgPath)
        {
            var imgFileName = DateTime.Now.ToFileTimeUtc() + "." + HtmlHelpers.GetExt(picture.FileName);

            if (System.IO.File.Exists(Server.MapPath(imgPath + "/" + imgFileName)))
            {
                System.IO.File.Delete(Server.MapPath(imgPath + "/" + imgFileName));
            }
            picture.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
            return imgFileName;
        }
        private string AddResizeImageToFolder(HttpPostedFileBase picture, string imgPath, int width, int height)
        {
            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(picture.FileName);

            if (System.IO.File.Exists(Server.MapPath(imgPath + "/" + imgFileName)))
            {
                System.IO.File.Delete(Server.MapPath(imgPath + "/" + imgFileName));
            }
            var newImage = Image.FromStream(picture.InputStream);
            var fixSizeImage = HtmlHelpers.FixedSize(newImage, width, height, true);
            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
            return imgFileName;
        }
        public bool CheckExtension(string extension, string extensions)
        {
            var result = false;

            var reg = new Regex(extensions, RegexOptions.Compiled);

            var m = reg.Match(extension.ToLower());
            if (m.Success)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region Banner
        [HttpGet]
        public ActionResult ListBanner()
        {
            return View(_db.Banners.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdateBanner(int? id)
        {
            Banner banner = _db.Banners.SingleOrDefault(x => x.Id == id);
            if (id > 0 && banner == null) return HttpNotFound();
            return View(banner);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrUpdateBanner(Banner banner, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (!UploadImageBanner(banner, picture))
                {
                    ModelState.AddModelError("", @"Hãy chọn ảnh");
                    return View(banner);
                }
                _db.Banners.AddOrUpdate(banner);
                _db.SaveChanges();
                return RedirectToAction(nameof(ListBanner));
            }
            return View(banner);
        }
        private bool CheckImageBanner(Banner banner, HttpPostedFileBase picture)
        {
            if (banner.Id == 0 && picture == null) return false;

            if (banner.Id > 0)
            {
                Banner banner1 = _db.Banners.SingleOrDefault(x => x.Id == banner.Id);
                if (banner1.Image == null && picture == null) return false;
                if (banner1.Image != null)
                {
                    banner.Image = banner1.Image;
                }
            }
            return true;

        }

        private bool UploadImageBanner(Banner banner, HttpPostedFileBase picture)
        { 
            if (!CheckImageBanner(banner, picture)) return false;
            if (picture != null)
            {
                var imgPath = "/Images/Banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(picture.FileName);

                if (System.IO.File.Exists(Server.MapPath(imgPath+ "/" + imgFileName)))
                {
                    System.IO.File.Delete(Server.MapPath(imgPath + "/" + imgFileName));
                }
                var newImage = Image.FromStream(picture.InputStream);
                var fixSizeImage = HtmlHelpers.FixedSize(newImage, banner.Width, banner.Height, true);
                HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                if (banner.Image != null)
                {
                    if (banner.Image.Length > 0)
                    {
                        HtmlHelpers.DeleteFile(Server.MapPath("/Images/Banners/" + banner.Image));
                    }
                }
                banner.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
            }
            return true;
        }
        [HttpPost]
        public JsonResult DeleteBanner(int id)
        {
            Banner banner = _db.Banners.SingleOrDefault(x => x.Id == id);
            if (banner == null && id > 0) return Json(false, JsonRequestBehavior.AllowGet);
            if(banner.Image != null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/Banners/" + banner.Image));
            }
            _db.Banners.Remove(banner);
            _db.SaveChanges();
            return Json(true);
        }
        //[HttpPost]
        //public JsonResult DeleteImageBanner(int id)
        //{
        //    Banner banner = _db.Banners.SingleOrDefault(x => x.Id == id);
        //    if (banner == null && id > 0) return Json(false, JsonRequestBehavior.AllowGet);
        //    if (banner.Image != null)
        //    {
        //        HtmlHelpers.DeleteFile(Server.MapPath("/Images/Banners/" + banner.Image));
        //    }
        //    banner.Image = null;
        //    _db.SaveChanges();
        //    return Json(true);
        //}

        #endregion

        #region Config
        public ActionResult ListConfigSite()
        {
            return View(_db.ConfigSites.ToList());
        }


        [HttpGet]
        public ActionResult AddOrUpdateConfig(int? id)
        {
            ConfigSite configSite = _db.ConfigSites.SingleOrDefault(x => x.Id == id);
            if (configSite == null && id > 0) return HttpNotFound();
            return View(configSite);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateConfig(ConfigSite configSite, HttpPostedFileBase pictureTop,HttpPostedFileBase pictureBottom)
        {
            if (ModelState.IsValid)
            {
                
                if (!UploadImageConfig(configSite,pictureTop,pictureBottom))
                {
                    ModelState.AddModelError("", @"Hãy chọn logo");
                    return View(configSite);
                }

                _db.ConfigSites.AddOrUpdate(configSite);
                _db.SaveChanges();

                HttpContext.Application["ConfigSite"] = configSite;

                return RedirectToAction(nameof(ListConfigSite));
            }
            return View(configSite);
        }
        private bool CheckConfig(ConfigSite configSite, HttpPostedFileBase pictureTop, HttpPostedFileBase pictureBottom)
        {
            if (configSite.Id == 0 && (pictureTop == null || pictureBottom == null)) return false;

            if (configSite.Id > 0)
            {
                ConfigSite configSite1 = _db.ConfigSites.SingleOrDefault(x => x.Id == configSite.Id);

                if ((configSite1.LogoBottom == null || configSite1.LogoTop == null) && (pictureTop == null || pictureBottom == null)) return false;
                if (configSite1.LogoBottom != null && configSite1.LogoTop!=null)
                {
                    configSite.LogoBottom = configSite1.LogoBottom;
                    configSite.LogoTop = configSite1.LogoTop;
                }
            }
            return true;

        }
        private bool UploadImageConfig(ConfigSite configSite, HttpPostedFileBase pictureTop, HttpPostedFileBase pictureBottom)
        { 
            if (!CheckConfig(configSite, pictureTop, pictureBottom)) return false;
            if (pictureTop != null || pictureBottom!=null)
            { 
                var imgPath = "/Images/ConfigSites/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                configSite.LogoTop = DateTime.Now.ToString("yyyy/MM/dd") + "/" + AddImageToFolder(pictureTop, imgPath);
                configSite.LogoBottom = DateTime.Now.ToString("yyyy/MM/dd")+"/"+ AddImageToFolder(pictureBottom, imgPath);
            }
            return true;
        } 
        [HttpPost]
        public JsonResult DeleteCofig(int id)
        {
            ConfigSite configSite = _db.ConfigSites.SingleOrDefault(x => x.Id == id);
            if (configSite == null && id > 0) return Json(false, JsonRequestBehavior.AllowGet);
            if (configSite.LogoTop != null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/ConfigSite/" + configSite.LogoTop));
            }
            if (configSite.LogoBottom!=null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/ConfigSite/" + configSite.LogoBottom));
            } 
            _db.ConfigSites.Remove(configSite);
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UserRole
        #region Login-logout
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginAction()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginAction(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserRole UserRole = _db.UserRoles.Where(x => x.UserName == model.UserName).FirstOrDefault();
                if (UserRole == null) return Content("<script>alert('Sai tài khoản');</script>");
                if (!HtmlHelpers.VerifyHash(model.PassWord, "SHA256", UserRole.PassWord))
                    return Content("<script>alert('Sai mật khẩu');window.location.href='/UserRole/LoginAction'</script>");
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return Content("<script>alert('Đăng nhập thành công'); window.location.href='/UserRole/Index'</script>");
            }
            return View(model);
        }

        public ActionResult LogoutAction()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(nameof(LoginAction));
        }
        #endregion
        private UserRole GetUserRole()
        {
            var ticket = Request.Cookies[FormsAuthentication.FormsCookieName];
            var authenTicket = FormsAuthentication.Decrypt(ticket.Value);
            UserRole UserRole = _db.UserRoles.Where(x => x.UserName == authenTicket.Name).FirstOrDefault();
            return UserRole;
        }

        public ActionResult ListUserRole()
        {
            return View(_db.UserRoles.ToList());
        }

        #region Add-update-delete
        [HttpGet]
        public ActionResult AddOrUpdateUserRole(int? id)
        {
            UserRole UserRole = _db.UserRoles.SingleOrDefault(x => x.Id == id);
            if (id > 0 && UserRole == null) return HttpNotFound();
            if (UserRole != null)
            {
                if (UserRole.UserName == "Admin" || UserRole.UserName == "admin")
                {
                    return RedirectToAction(nameof(ListUserRole));
                }
            }
            return View(UserRole);
        }

        [HttpPost]
        public ActionResult AddOrUpdateUserRole(UserRole UserRole, string confimPass)
        {
            if (UserRole.UserName == "Admin" || UserRole.UserName == "admin")
            {
                ModelState.AddModelError("", @"Không được lấy tên là admin hoặc Admin");
                return View(UserRole);
            }
            if (ModelState.IsValid)
            {
                if (UserRole.PassWord != confimPass)
                {
                    ModelState.AddModelError("", @"Mật khẩu xác nhận không khớp nhau");
                    return View(UserRole);
                }
                UserRole.PassWord = HtmlHelpers.ComputeHash(UserRole.PassWord, "SHA256", null);
                _db.UserRoles.AddOrUpdate(UserRole);
                _db.SaveChanges();
                FormsAuthentication.SignOut();
                return RedirectToAction(nameof(ListUserRole));

            }
            return View(UserRole);
        }
        [HttpPost]
        public JsonResult DeleteUserRole(int id)
        {
            UserRole UserRole = _db.UserRoles.SingleOrDefault(x => x.Id == id);
            if (UserRole == null || UserRole.UserName=="Admin")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            _db.UserRoles.Remove(UserRole);
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Chang PassWord

        [HttpGet]
        public ActionResult ChangePassWord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassWord(ChangPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserRole UserRole = GetUserRole();
                if (!HtmlHelpers.VerifyHash(model.OldPassWord, "SHA256", UserRole.PassWord))
                {
                    ModelState.AddModelError("", @"Mật khẩu cũ không chính xác!");
                    return View(model);
                }
                if (model.NewPassWord != model.ConfimPassword)
                {
                    ModelState.AddModelError("", @"Mật khẩu mới không khớp!");
                    return View(model);
                }
                UserRole.PassWord = HtmlHelpers.ComputeHash(model.NewPassWord, "SHA256", null);
                _db.UserRoles.AddOrUpdate(UserRole);
                _db.SaveChanges();

                return RedirectToAction(nameof(LoginAction));
            }
            return View(model);
        }
        #endregion
        #endregion

        #region FeedBack
        public ActionResult ListFeedBack()
        {
            return View(_db.FeedBacks.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdateFeedBack(int? id)
        {
            FeedBack feedBack = _db.FeedBacks.SingleOrDefault(x=>x.ID==id);
            if (feedBack == null && id > 0) return HttpNotFound();
            return View(feedBack);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateFeedBack(FeedBack feedBack,HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (!UploadImageFeedBack(feedBack, picture))
                {
                    ModelState.AddModelError("", @"Hãy chọn ảnh");
                    return View(feedBack);
                }
                _db.FeedBacks.AddOrUpdate(feedBack);
                _db.SaveChanges();
                return RedirectToAction(nameof(ListFeedBack));
            }
            return View(feedBack);
        }
        private bool CheckImageFeedBack(FeedBack feedBack, HttpPostedFileBase picture)
        {
            if (feedBack.ID == 0 && picture == null) return false;

            if (feedBack.ID > 0)
            {
                FeedBack feedBack1 = _db.FeedBacks.SingleOrDefault(x => x.ID == feedBack.ID);
                if (feedBack1.Avatar == null && picture == null) return false;
                if (feedBack1.Avatar != null)
                {
                    feedBack.Avatar = feedBack1.Avatar;
                }
            }
            return true;
        }
        private bool UploadImageFeedBack(FeedBack feedBack, HttpPostedFileBase picture)
        {
            bool res = CheckImageFeedBack(feedBack, picture);
            if (res == false) return false;
            if (picture != null)
            {
                var imgPath = "/Images/FeedBacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                if (feedBack.Avatar != null)
                {
                    HtmlHelpers.DeleteFile(Server.MapPath("/Images/FeedBacks/" + feedBack.Avatar));
                }
                feedBack.Avatar = DateTime.Now.ToString("yyyy/MM/dd") + "/" + AddImageToFolder(picture,imgPath);
            }
            return true;
        }

        public JsonResult DeleteImageFeedBack(int? id)
        {
            FeedBack feedBack = _db.FeedBacks.SingleOrDefault(x => x.ID == id);
            if (feedBack == null && id > 0) return Json(false, JsonRequestBehavior.AllowGet);
            if (feedBack.Avatar != null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/FeedBacks/" + feedBack.Avatar));
            }
            feedBack.Avatar = null;
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteFeedBack(int? id)
        {
            FeedBack feedBack = _db.FeedBacks.SingleOrDefault(x => x.ID == id);
            if (feedBack == null && id > 0) return Json(false, JsonRequestBehavior.AllowGet);
            if (feedBack.Avatar != null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/FeedBacks/" + feedBack.Avatar));
            }
            _db.FeedBacks.Remove(feedBack);
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Post

        public ActionResult ListPost(int? page)
        {
            return View(_db.Posts.OrderBy(x => x.DisPlayOrder).ToPagedList(page ?? 1, 15));
        }

        #region Add-Update post

        [HttpGet]
        public ActionResult AddOrUpdatePost(int? id)
        {
            Post post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (id > 0 && post == null) return HttpNotFound();
            return View(post);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdatePost(Post model, HttpPostedFileBase picture, int PostCategoryID)
        {
            if (ModelState.IsValid)
            {
                if (!UploadImagePost(model, picture))
                {
                    ViewBag.Table_PostCategory = _db.Table_PostCategory.ToList();
                    ModelState.AddModelError("", @"Hãy chọn ảnh!");
                    return View(model);
                }
                _db.Posts.AddOrUpdate(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(ListPost));
            }
            ViewBag.Table_PostCategory = _db.Table_PostCategory.ToList();
            return View(model);
        }

        private bool CheckPost(Post post, HttpPostedFileBase picture)
        {
            if (post.Id == 0 && picture == null) return false;

            if (post.Id > 0)
            {
                Post post1 = _db.Posts.SingleOrDefault(x => x.Id == post.Id);
                if (post1.Image == null && picture == null) return false;

                if (post1.Image != null)
                {
                    post.Image = post1.Image;
                }
            }
            return true;

        }

        private bool UploadImagePost(Post post, HttpPostedFileBase picture)
        { 
            if (!CheckPost(post, picture)) return false;
            if (picture != null)
            {
                var imgPath = "/Images/Posts/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath)); 
                if (post.Image != null)
                {
                    HtmlHelpers.DeleteFile(Server.MapPath("/Images/Posts/" + post.Image));
                }
                post.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + AddImageToFolder(picture,imgPath);
            }
            return true;
        }

        //public ActionResult DeletePost(int id)
        //{
        //    Post post = _db.Posts.SingleOrDefault(x => x.Id == id);
        //    if (id > 0 && post == null) return Json(false, JsonRequestBehavior.AllowGet);
        //    _db.Posts.Remove(post);
        //    _db.SaveChanges();
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}
        #endregion


        #region delete-deleteImage post
        public JsonResult DeletePost(int id)
        {
            Post post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (id > 0 && post == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (post.Image != null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/Posts/" + post.Image));
            }
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return Json(true);
        }
        public JsonResult DeleteImagePost(int id)
        {
            Post post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (id > 0 && post == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (post.Image != null)
            {
                HtmlHelpers.DeleteFile(Server.MapPath("/Images/Posts/" + post.Image));
            }
            post.Image = "";
            _db.SaveChanges();
            return Json(true);
        }
        #endregion

        #endregion

        #region PostCategory
        public ActionResult ListPostCategory()
        {
            return View(_db.Table_PostCategory.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpDatePostCategory(int? id)
        {
            PostCategory postCategory = _db.Table_PostCategory.SingleOrDefault(a => a.ID == id);
            if (id > 0 && postCategory == null) return HttpNotFound();
            return View(postCategory);
        }
        [HttpPost]
        public ActionResult AddOrUpDatePostCategory(PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Table_PostCategory.AddOrUpdate(postCategory);
                _db.SaveChanges();
                return RedirectToAction(nameof(ListPostCategory));
            }
            return View(postCategory);
        }
        [HttpPost]
        public JsonResult DeletePostCategory(int catId)
        {
            var postCategory = _db.Table_PostCategory.SingleOrDefault(a => a.ID == catId);
            if (postCategory == null) return Json(false);
            _db.Table_PostCategory.Remove(postCategory);
            _db.SaveChanges();
            return Json(true);
        }
        #endregion

        #region Product
        [HttpGet]
        public ActionResult ListProduct(int? page)
        {
            return View(_db.Product.OrderBy(x=>x.DisplayOrder).ToPagedList(page ?? 1,15));
        }
        [HttpGet]
        public ActionResult AddOrUpdateProduct(int? id)
        {
            Product product = _db.Product.SingleOrDefault(x => x.Id == id);
            if (id > 0 && product == null) return HttpNotFound();
            return View(product);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateProduct(Product product,List<HttpPostedFileBase> picture)
        {
            if (ModelState.IsValid)
            {

            }
            return View(product);
        }
        private bool CheckImageProduct(Product product,List<HttpPostedFileBase> pictures)
        {
            if (product.Id == 0 && pictures.Count() == 0) return false;
            if (product.Id > 0)
            {
                Product product1 = _db.Product.Find(product.Id);
                if (product1.Images == null && pictures.Count() == 0) return false;
                if (product1.Images != null)
                {
                    product.Images = product1.Images;
                }
            }
            return true;
        }
        private bool UploadImageProduct(Product product, List<HttpPostedFileBase> pictures)
        {
            if (!CheckImageProduct(product, pictures)) return false;
            if (product.Id > 0)
            {
                if (product.Images != null)
                {
                    string[] str = product.Images.Split(';');
                    if (str.Length + pictures.Count() > 10) return false;
                }
            }
            if (pictures.Count() > 10) return false;
            foreach(HttpPostedFileBase picture in pictures)
            {
                if (picture != null)
                {
                    if (picture.ContentLength <= (4000 * 1024) && CheckExtension(Path.GetExtension(picture.FileName), ".jpg|.png|.gif|.jpeg"))
                    {
                        var imgPath = "/Images/Products/" + DateTime.Now.ToString("yyyy/MM/dd");
                        HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                        product.Images += DateTime.Now.ToString("yyyy/MM/dd") + "/" + AddResizeImageToFolder(picture,imgPath,255,255) + ";";
                    }
                }
            }
            return true;
        }
        [HttpPost]
        public JsonResult DeleteProduct(int? id)
        {
            Product product = _db.Product.SingleOrDefault(x => x.Id == id);
            if (id > 0 && product == null) return Json(false, JsonRequestBehavior.AllowGet);
            if(product.Images != null)
            {
                string[] strs = product.Images.Split(';');
                foreach(string str in strs)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Images/Products/" + str)) )
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/Products/" + str));
                    }
                }
            }
            _db.Product.Remove(product);
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteImageProduct(int productId,int locationImage)
        {
            Product product = _db.Product.SingleOrDefault(x => x.Id == productId);
            if (productId > 0 && product == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (product.Images != null)
            {
                string[] strs = product.Images.Split(';');
                for(int i = 0; i < strs.Length; i++)
                {
                    if (i == locationImage)
                    {
                        if (System.IO.File.Exists(Server.MapPath("/Images/Products/" + strs[i])))
                        {
                            System.IO.File.Delete(Server.MapPath("/Images/Products/" + strs[i]));
                        }
                    }
                    else
                    {
                        product.Images += strs[i]+";";
                    }
                }
            } 
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Productcategory
        [HttpGet]
        public ActionResult ListProductCategory()
        {
            return View(_db.Table_ProductCategory.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdateProductCategory(int? id)
        {
            ProductCategory productCategory = _db.Table_ProductCategory.SingleOrDefault(x => x.ID == id);
            if (id > 0 && productCategory == null) return HttpNotFound();
            return View(productCategory);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateProductCategory(ProductCategory productCategory,List<int> TradeMarkIds)
        {
            if (ModelState.IsValid)
            {
                _db.Table_ProductCategory.Add(productCategory);
                _db.SaveChanges();
                AddTagForProductCategory(productCategory.ID,TradeMarkIds);
                return RedirectToAction(nameof(ListProductCategory));
            }
            return View(productCategory);
        }
        private void AddTagForProductCategory(int id, List<int> TradeMarkIds)
        {
            List<TagProductCategory> tagProductCategories = _db.Tag_ProductCategry_TradeMarks.Where(x => x.ProductCategoryID == id).ToList();
            if (tagProductCategories.Count() > 0)
            {
                _db.Tag_ProductCategry_TradeMarks.RemoveRange(tagProductCategories);
                _db.SaveChanges();
            }
            foreach(int TradeMarkID in TradeMarkIds)
            {
                TradeMark tradeMark = _db.TradeMarks.SingleOrDefault(x => x.Id == TradeMarkID);
                if (tradeMark != null)
                {
                    TagProductCategory newTag = new TagProductCategory()
                    {
                        TradeMarkID = tradeMark.Id,
                        ProductCategoryID = id
                    };
                    _db.Tag_ProductCategry_TradeMarks.Add(newTag);
                }
            }
            _db.SaveChanges();
        }


        [HttpPost]
        public JsonResult DeleteProductCategory(int? id)
        {
            ProductCategory productCategory = _db.Table_ProductCategory.SingleOrDefault(x => x.ID == id);
            if (id > 0 && productCategory == null) return Json(false, JsonRequestBehavior.AllowGet);
            _db.Table_ProductCategory.Remove(productCategory);
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TradeMark
        public ActionResult ListTradeMark()
        {
            return View(_db.TradeMarks.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdateTradeMark(int? id)
        {
            TradeMark tradeMark = _db.TradeMarks.SingleOrDefault(x => x.Id == id);
            if (tradeMark == null && id > 0) return HttpNotFound();
            return View(tradeMark);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateTradeMark(TradeMark tradeMark,HttpPostedFileBase picture,List<int> ProductCategoryID)
        {
            if (ModelState.IsValid)
            {
                if (!UploadeImageTradeMark(tradeMark, picture))
                {
                    ModelState.AddModelError("", @"Hãy chọn ảnh");
                    return View(tradeMark);
                }
                _db.TradeMarks.Add(tradeMark);
                _db.SaveChanges();
                AddTagForTrandeMark(tradeMark.Id, ProductCategoryID);
                return RedirectToAction(nameof(ListTradeMark));
            }
            return View(tradeMark);
        }
        private bool CheckImageTradeMark(TradeMark tradeMark, HttpPostedFileBase picture)
        {
            if (tradeMark.Id == 0 && picture == null) return false;
            if (tradeMark.Id > 0)
            {
                TradeMark tradeMark1= _db.TradeMarks.SingleOrDefault(x => x.Id == tradeMark.Id);
                if (tradeMark1.Avatar != null) tradeMark.Avatar = tradeMark1.Avatar;
            }
            return true;
        }
        private bool UploadeImageTradeMark(TradeMark tradeMark, HttpPostedFileBase picture)
        {
            if (!CheckImageTradeMark(tradeMark,picture)) return false;

            if (picture != null)
            {
                var imgPath = "/Images/TrandMarks/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                tradeMark.Avatar = DateTime.Now.ToString("yyyy/MM/dd") + "/" + AddResizeImageToFolder(picture, imgPath, 186, 113); 
            }
            return true;
        }
        private void AddTagForTrandeMark(int id,List<int> ProductCategoryId)
        {
            List<TagProductCategory> tagProductCategories = _db.Tag_ProductCategry_TradeMarks.Where(x => x.TradeMarkID == id).ToList();
            if(tagProductCategories.Count()>0)
            {
                _db.Tag_ProductCategry_TradeMarks.RemoveRange(tagProductCategories);
                _db.SaveChanges();
            }
            foreach(int catID in ProductCategoryId)
            {
                ProductCategory productCategory = _db.Table_ProductCategory.SingleOrDefault(x => x.ID == catID);
                if (productCategory != null)
                {
                    TagProductCategory newTag = new TagProductCategory()
                    {
                        TradeMarkID = id,
                        ProductCategoryID = productCategory.ID
                    };
                    _db.Tag_ProductCategry_TradeMarks.Add(newTag);
                }
            }
            _db.SaveChanges();
        }

        #endregion
    }
}