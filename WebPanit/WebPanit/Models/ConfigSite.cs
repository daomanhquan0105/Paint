using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebPanit.Models
{
    public class ConfigSite
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Facebook"), Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Facebook { get; set; } 

        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Youtube"), Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Youtube { get; set; }

        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Instagram"), Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Instagram { get; set; }

        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Telegram"), Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string Twitter { get; set; }

        [StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), Display(Name = "Đường dẫn Google plus"), Url(ErrorMessage = "Đường dẫn không chính xác"), UIHint("TextBox")]
        public string GooglePlus { get; set; }

        [Display(Name = "Địa chỉ"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại"), StringLength(15, ErrorMessage = "Tối đa 15s ký tự"), UIHint("TextBox")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email"), EmailAddress(ErrorMessage = "Email không hợp lệ"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Email { get; set; }

        [Display(Name = "Mật khâu"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Password { get; set; }

        [Display(Name = "Hình ảnh"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự")]
        public string LogoTop { get; set; }

        [Display(Name = "Hình ảnh"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự")]
        public string LogoBottom { get; set; }

        [Display(Name = "Hướng dẫn thanh toán"), Column(TypeName = "ntext"), UIHint("EditorBox")]
        public string Payment { get; set; }

        [Display(Name = "Giúp đỡ và tư vấn"), Column(TypeName = "ntext"), UIHint("EditorBox")]
        public string Helper { get; set; }

        [Display(Name = "Vận chuyển và trả hàng"), Column(TypeName = "ntext"), UIHint("EditorBox")]
        public string  Transport { get; set; }

        [Display(Name = "Chính sách hoàn tiền"), Column(TypeName = "ntext"), UIHint("EditorBox")]
        public string RefundPolicy { get; set; } 

        [Display(Name = "Nội dung chân trang"), Column(TypeName = "ntext"), UIHint("TextBox")]
        public string CopyRight { get; set; }

        public bool Active { get; set; }
    }
}