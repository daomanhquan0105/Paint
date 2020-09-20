using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPanit.Models;
using WebPanit.ViewModel;
using System.IO;
using PagedList;
using System.Threading.Tasks;
using Helpers;
using System.Web.Configuration;

namespace WebPanit.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataEntities _db = new DataEntities();
        private string Email => WebConfigurationManager.AppSettings["email"];
        private string Password => WebConfigurationManager.AppSettings["password"];
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                TradeMarks = _db.TradeMarks.Where(x => x.Active == true && x.FlagHome == true && x.Avatar!=null).ToList(),
                Banners = _db.Banners.Where(x => x.Active == true && x.Image!=null).ToList(),
                LibraryImages = _db.LibraryImages.Where(x => x.Active == true && x.FlagHome == true && x.Avatar!=null).ToList(),
                FeedBacks = _db.FeedBacks.Where(x => x.Active == true && x.FlagHome == true && x.Avatar != null).ToList()
            };
            return View(model);
        } 

        [Route("Danh-sach-san-pham/{name}")]
        public ActionResult ListProduct(int? page, int? tradeMarkID, int? productCategoryID)
        {
            if (tradeMarkID != null)
            {
                ProductViewModel model = new ProductViewModel()
                {
                    Products = _db.Products.Where(x => x.TradeMarkID == tradeMarkID && x.Active == true).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12),
                    TradeMark = _db.TradeMarks.SingleOrDefault(x => x.Id == tradeMarkID),
                    ProductCategory = null
                };
                return View(model);
            }
            
            if(productCategoryID!=null)
            {
                ProductViewModel model = new ProductViewModel()
                {
                    Products = _db.Products.Where(x => x.ProductCategoryID == productCategoryID && x.Active == true).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12),
                    TradeMark = null,
                    ProductCategory = _db.Table_ProductCategory.SingleOrDefault(x=>x.ID==productCategoryID)
                };
                return View(model);
            }
            ProductViewModel modelAll = new ProductViewModel()
            {
                Products = _db.Products.Where(x=>x.Active == true).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12),
                TradeMark = null,
                ProductCategory = _db.Table_ProductCategory.SingleOrDefault(x => x.ID == productCategoryID)
            }; 
            return View(modelAll);
        
        }
        [Route("Chi-tiet-san-pham/{name}-{id}")]
        public ActionResult ProductDetail(long id)
        {
            if (id == 0) return HttpNotFound();
            Product product = _db.Products.SingleOrDefault(x => x.Id == id);
            if (product == null) return HttpNotFound();
            return View(product);
        }
        [Route("Danh-sach-san-pham-tim-kiem")]
        [HttpGet]
        public ActionResult ProductSearch(int? page,int? ProductCategoryID,string ProductName)
        {
            if(ProductCategoryID!=null && ProductName!=null)
            {
                IPagedList<Product> products = _db.Products.Where(x => x.Active && x.ProductCategoryID == ProductCategoryID && x.Name.Contains(ProductName)).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12);
                return View(products);
            }
            if (ProductCategoryID == null && ProductName != null)
            {
                IPagedList<Product> products = _db.Products.Where(x => x.Active && x.Name.Contains(ProductName)).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12);
                return View(products);
            }
            IPagedList<Product> productAll = _db.Products.Where(x => x.Active).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12);
            return View(productAll) ;
        }

        [Route("Danh-sach-mau-son")]
        public ActionResult TableColorPaint(int? page)
        {
            return View(_db.TradeMarks.Where(x => x.Active == true).OrderBy(x => x.DisplayOrder).ToPagedList(page ?? 1, 12));
        }
        [Route("Danh-sach-bai-dang/{name}-{PostCategoryId}")]
        public ActionResult ListPost(int? page,int? PostCategoryId)
        {
            if (PostCategoryId != null)
            {
                return View(_db.Posts.Where(x => x.Active && x.PostCategoryID==PostCategoryId).OrderBy(x => x.DisPlayOrder).ToPagedList(page ?? 1, 12));
            }
            return View(_db.Posts.Where(x => x.Active).OrderBy(x => x.DisPlayOrder).ToPagedList(page ?? 1, 12));
        }
        [Route("chi-tiet-bai-dang/{name}-{id}")]
        public ActionResult PostDetail(int id)
        {
            Post post = _db.Posts.SingleOrDefault(x => x.Id == id);
            if (post == null) return HttpNotFound();
            return View(post);
        }
        [Route("Thong-tin-lien-he")]
        [HttpGet]
        public ActionResult ContactView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ContactView(Contact contact)
        {
            if (ModelState.IsValid)
            {
                SendEmailForContact(contact);
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [Route("Bao-gia-son")]
        public ActionResult PaintPrice()
        {
            PaintColor model = new PaintColor()
            {
                tradeMarks = _db.TradeMarks.ToList(),
                Posts = _db.Posts.Where(x => x.Active && x.PostCategory.CategoryName.Contains("Màu sơn")).ToList()
            };
            return View(model);
        }
        [Route("Huong-dan-mua-hang")]
        public ActionResult PaymentView()
        {
            return View();
        }
        [Route("Dich-vu-website")]
        public ActionResult ServiceView()
        {
            return View(_db.Posts.Where(x => x.Active && x.PostCategory.CategoryName.Contains("dịch vụ")).ToList());
        }
        [HttpPost]
        public JsonResult SendEmail(ReceiveEmail receiveEmail) 
        {
            if (ModelState.IsValid)
            {
                SendEmailForReceiveEmail(receiveEmail);
                _db.ReceiveEmails.Add(receiveEmail);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false,JsonRequestBehavior.AllowGet);
        }
        [Route("dat-hang/{name}-{id}")]
        [HttpGet]
        public ActionResult OrderProduct(long id)
        {
            Product product = _db.Products.SingleOrDefault(x => x.Id == id);
            if (product == null) return HttpNotFound();
            OrderViewModel model = new OrderViewModel()
            {
                Product = product,
                Member = new Member(),
                Quantity=1,
                ProductID=id
            };
            return View(model);
        }
        [HttpPost]
        public JsonResult OrderProduct(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = _db.Products.SingleOrDefault(x => x.Id == model.ProductID);
                model.Member.Active = true;
                _db.Members.Add(model.Member);
                _db.SaveChanges();
                Order order = new Order()
                {
                    MemberID = model.Member.Id,
                    Price = product.Price * model.Quantity,
                    CreateDate = DateTime.Now,
                    ProductID = product.Id,
                    Status = false,
                    ShipDate = DateTime.Now.AddDays(10),
                    Quantity=model.Quantity,
                    
                };
                product.Quantity -= model.Quantity;
                _db.Orders.Add(order);
                _db.SaveChanges();
                order.OrderCode = DateTime.Now.ToString("yyyyMMddHHmm") + "C" + order.ID;
                _db.SaveChanges();

                SendEmailForCustomer(order); 
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [Route("Danh-sach-khach-hang-tieu-bieu")]
        public ActionResult TypicalCustomerView()
        {
            return View(_db.TypicalCustomers.Where(x => x.Active == true && x.Avatar != null).ToList());
        }
        [Route("Thu-vien-anh")]
        public ActionResult AlbumImageView()
        {
            return View(_db.LibraryImages.Where(x => x.Active == true && x.Avatar != null).ToList());
        }

        #region function Send Email
        private void SendEmailForCustomer(Order order)
        {
            var sb = "<p style='font-size:16px'>Thông tin đơn hàng gửi từ website " + Request.Url?.Host + "</p>";
            sb += "<p>Mã đơn hàng: <strong>" + order.OrderCode + "</strong></p>";
            sb += "<p>Họ và tên: <strong>" + order.Member.FullName + "</strong></p>";
            sb += "<p>Địa chỉ: <strong>" + order.Member.Address + "</strong></p>";
            sb += "<p>Email: <strong>" + order.Member.Email + "</strong></p>";
            sb += "<p>Điện thoại: <strong>" + order.Member.Phone + "</strong></p>"; 
            sb += "<p>Ngày đặt hàng: <strong>" + order.CreateDate.ToString("dd-MM-yyyy HH:ss") + "</strong></p>";
            sb += "<p>Ngày giao hàng: <strong>" + order.ShipDate.ToString("dd-MM-yyyy") + "</strong></p>";
            sb += "<p>Hình thức giao hàng: <strong>Giao hàng</strong></p>"; 
            sb += "<p>Thông tin đơn hàng</p>";
            sb += "<table border='1' cellpadding='10' style='border:1px #ccc solid;border-collapse: collapse'>" +
                  "<tr>" +
                  "<th>Ảnh sản phẩm</th>" +
                  "<th>Tên sản phẩm</th>" +
                  "<th>Số lượng</th>" +
                  "<th>Giá tiền</th>" +
                  "<th>Thành tiền</th>" +
                  "</tr>";
            string str = order.Product.Images.Split(';')[0];
            sb += "<tr>" +
                      "<td><img src = 'https://" + Request.Url?.Host + "/images/Products/" + str +"' class='w100' /></td>" +
                      "<td>" + order.Product.Name;

            sb += "</td>" +
                  "<td style='text-align:center'>" + order.Quantity + "</td>" +
                  "<td style='text-align:center'>" + Convert.ToDecimal(order.Product.Price).ToString("N0") + "</td>" +
                  "<td style='text-align:center'>" + Convert.ToDecimal(order.Price).ToString("N0") + " đ</td>" +
                  "</tr>"; 

            sb += "<tr><td colspan='5' style='text-align:right'><strong>Tổng tiền: " + Convert.ToDecimal(order.Price).ToString("N0") + " đ</strong></td></tr>";
            sb += "</table>";
            sb += "<p>Cảm ơn bạn đã tin tưởng và mua hàng của chúng tôi.</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", "[" + order.OrderCode + "] Đơn đặt hàng từ website Sơn skey", sb, "SonSkey.webPaint@gmail.com", Email, Email, Password, "Đặt Hàng Online", order.Member.Email, "SonSkey.webPaint@gmail.com"));
        }

        private void SendEmailForReceiveEmail(ReceiveEmail receiveEmail)
        {
            var sb = "<p style='font-size:16px'>Thông tin được gửi gửi từ website " + Request.Url?.Host + "</p>";
            sb += "<p>Đến: <strong>" + receiveEmail.Email + "</strong></p>";
            sb += "<p>Cảm ơn bạn đã tin tưởng chúng tôi</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", "Thông tin được gửi từ website Sơn skey", sb, "SonSkey.webPaint@gmail.com", Email, Email, Password, "Nhận thông tin", receiveEmail.Email, "SonSkey.webPaint@gmail.com"));
        }
        private void SendEmailForContact(Contact contact)
        {
            var sb = "<p style='font-size:16px'>Thông tin được gửi gửi từ website " + Request.Url?.Host + "</p>";
            sb += "<p>Đến: <strong>" + contact.Email + "</strong></p>";
            sb += "<p>Cảm ơn bạn đã để liên hệ lại cho chúng tôi</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", "Thông tin được gửi từ website Sơn skey", sb, "SonSkey.webPaint@gmail.com", Email, Email, Password, "Thông tin liên hệ", contact.Email, "SonSkey.webPaint@gmail.com"));
        }
        #endregion
        #region Partial
        [ChildActionOnly]
        public PartialViewResult HeaderPartial()
        { 
            return PartialView(_db.Table_ProductCategory.Where(x=>x.Active==true).ToList());
        }
        [ChildActionOnly]
        public PartialViewResult FooterPartial()
        { 
            return PartialView(_db.PostCategoryParents.Where(x=>x.Active==true).OrderBy(x=>x.DisplayOrder).ToList());
        }
        [ChildActionOnly]
        public PartialViewResult ListTradeMarkPartial()
        {
            return PartialView(_db.TradeMarks.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult ProductCardPartial(Product product)
        {
            return PartialView(product);
        }
        [ChildActionOnly]
        public PartialViewResult ActiveProductForCategoryPartial(TradeMark tradeMark)
        {
            return PartialView(tradeMark);
        }
        [ChildActionOnly]
        public PartialViewResult MenuMobilePartial()
        {
            return PartialView(_db.TradeMarks.Where(x=>x.Active==true).ToList());
        }
        [ChildActionOnly]
        public PartialViewResult ProductDetailPartial(Product product)
        {
            return PartialView(product);
        }
        [ChildActionOnly]
        public PartialViewResult MenuXLPartial()
        {
            MenuXLViewModel model = new MenuXLViewModel()
            {
                TradeMarks = _db.TradeMarks.Where(x => x.Active == true).ToList(),
                Posts = _db.Posts.Where(x => x.Active == true && x.FlagHome == true).OrderBy(x => x.DisPlayOrder).Take(5).ToList()
            };
            return PartialView(model);
        }

        public PartialViewResult LasterPost()
        {
            return PartialView(_db.Posts.Where(x => x.Active == true && x.FlagHome == true).Take(15).ToList());
        }
        #endregion
    }
}