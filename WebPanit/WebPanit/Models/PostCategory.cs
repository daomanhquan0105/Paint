using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebPanit.Models
{
    public class PostCategory
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Tên danh mục"), Required(ErrorMessage = "Hãy nhập tên danh mục"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), UIHint("TextBox")]
        public string CategoryName { get; set; }

        [Display(Name = "Đường dẫn"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextBox")]
        public string Url { get; set; }

        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int DisplayOrder { get; set; }

        [DisplayName("Hiển thi?")]
        public bool Active { get; set; }

        [DisplayName("Hiện trang chủ?")]
        public bool ShowOnHome { get; set; }
        [DisplayName("Danh mục gốc")]
        public int? ParentPostCategoryID { get; set; }
        [ForeignKey("ParentPostCategoryID")]
        public virtual PostCategoryParent  PostCategoryParents { get; set; } 

        public virtual List<Post> Posts { get; set; }
        public PostCategory()
        {
            Active = true;
            ShowOnHome = true;
        }
    }

}