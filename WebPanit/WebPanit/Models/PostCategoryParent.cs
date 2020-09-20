using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPanit.Models
{
    public class PostCategoryParent
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên danh mục gốc"), Required(ErrorMessage = "Hãy nhập tên danh mục gốc"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
        public virtual List<PostCategory> PostCategories { get; set; }
    }
}