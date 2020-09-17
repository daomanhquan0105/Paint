using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebPanit.Models
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tiêu đề"), StringLength(500, ErrorMessage = "Không được vượt quá 500"), UIHint("TextArea")]
        public string Subject { get; set; }
        [Display(Name = "Thông tin điều kiện, điều khoản"), Column(TypeName = "ntext"), UIHint("EditorBox"), Required(ErrorMessage = "Hãy nhập nội dung cho bài viết")]
        public string Content { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên"), UIHint("NumberBox")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public Condition()
        {
            Active = true;
        }
    }
}