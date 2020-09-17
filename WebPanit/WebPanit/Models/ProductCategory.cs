using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanit.Models
{
    public class ProductCategory
    {
        [Key]
        public int ID { get; set; }


        [DisplayName("Tên loại thuốc"), UIHint("TextBox")]
        [Required(ErrorMessage = "Hãy nhập tên cho loại thuốc")]
        [StringLength(250)]
        public string Name { get; set; }

        [DisplayName("Thứ tự hiện thị"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương")]
        [Required(ErrorMessage = "Hãy chọn thứ tự hiển thị"), UIHint("NumberBox")]
        public int DisplayOrder { get; set; }

        [DisplayName("Ngày tạo"), UIHint("DateTimePicker")]
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }

        [DisplayName("Show Menu ngang")]
        public bool ShowMenuVertical{ get; set; }

        [DisplayName("Show trang chủ")]
        public bool FlagHome { get; set; }

        public List<TagProductCategory> ListTagProductCategory { get; set; }

        public List<Product> Products { get; set; }

        public ProductCategory()
        {
            CreateDate = DateTime.Now;
            Active = true;
            FlagHome = true;
        }
    }
}
