using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebPanit.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Họ và tên"), StringLength(60, ErrorMessage = "Không được vượt quá 60"), UIHint("TextBox")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Hãy nhập email")]
        [Display(Name = "Email"), EmailAddress(ErrorMessage = "Email không hợp lệ"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Hãy nhập số điện thoại của bạn"), Display(Name = "Số điện thoại"), StringLength(15, ErrorMessage = "Tối đa 15s ký tự"), UIHint("TextBox")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tiêu đề"), StringLength(200, ErrorMessage = "Không được vượt quá 200"), UIHint("TextBox")]
        public string Subject { get; set; }
        
        [Display(Name = "Thông tin liên hệ"), UIHint("TextArea"), Required(ErrorMessage = "Hãy nhập nội dung cho bài viết")]
        public string Content { get; set; }
    }
}