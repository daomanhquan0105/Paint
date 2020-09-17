using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanit.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Tên sơn"),Required(ErrorMessage ="Hãy nhập tên cho sản phẩm"),StringLength(100,ErrorMessage ="Không được vượt quá 100 ký tự"),UIHint("TextBox")]
        public string Name { get; set; }

        [DisplayName("Mã sản phẩm"), UIHint("TextBox")]
        [Required(ErrorMessage = "Hãy nhập mã cho sản phẩm")]
        [StringLength(20)]
        public string Code { get; set; }
        
        [DisplayName("Danh sách hình ảnh")]
        public string Images { get; set; }

        [DisplayName("Đơn giá"), UIHint("NumberBox"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương")]
        [Required(ErrorMessage = "Hãy nhập giá cho sản phẩm")]
        public decimal Price { get; set; }

        [DisplayName("Giá gốc"), UIHint("NumberBox"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương")]
        public decimal OriginalPrice { get; set; }

        [DisplayName("Số lượng tồn"), UIHint("NumberBox")]
        [Required(ErrorMessage = "Nhập số lượng cho sản phẩm")]
        public int Quantity { get; set; }

        [DisplayName("Miêu tả ngắn"), UIHint("TextArea")]
        [Required(ErrorMessage = "Hãy miêu tả ngắn về sản phẩm")]
        [StringLength(500)]
        public string Description { get; set; }

        [DisplayName("Miêu tả chi tiết"), UIHint("EditorBox"), Required(ErrorMessage = "Hãy miêu tả chi tiết cho sản phẩm")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        [DisplayName("Chính sách bảo hành"), UIHint("EditorBox")]
        [Column(TypeName = "ntext")]
        public string WarrantyPolicy { get; set; }

        [DisplayName("Ngày tạo"), UIHint("DateTimePicker")]
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Thứ tự hiển thị"), UIHint("NumberBox"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương")]
        [Required(ErrorMessage = "Hãy nhập thứ tự hiện thị cho sản phẩm")]
        public int DisplayOrder { get; set; }

        //[DisplayName("Nhà sản xuất")]
        //[Required(ErrorMessage = "Chọn hãng sản xuất của sản phẩm")]
        //public int SupplierID { get; set; }

        //[Display(Name = "Xuất xứ"), StringLength(100, ErrorMessage = "Không được vượt quá 100 ký tự"), UIHint("TextBox")]
        //public string Origin { get; set; } 

        [DisplayName("Hiển thị")]
        public bool Active { get; set; }

        [DisplayName("Hiện trang chủ")]
        public bool FlagHome { get; set; }
        public int View { get; set; }

        public int ProductCategoryID { get; set; }
        [ForeignKey("ProductCategoryID")]
        public ProductCategory ProductCategory { get; set; }
        public Product()
        {
            CreateDate = DateTime.Now;
            Active = true;
            FlagHome = true;
            View = 0;
        }
    }
}
