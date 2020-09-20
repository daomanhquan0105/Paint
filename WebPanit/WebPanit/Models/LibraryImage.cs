using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPanit.Models
{
    public class LibraryImage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Hình ảnh"), StringLength(500)]
        public string Avatar { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên"), UIHint("NumberBox")]
        public int DisplayOrder { get; set; }
        [Display(Name ="Hiển thị")]
        public bool Active { get; set; }
        [Display(Name = "Hiện trang chủ")]
        public bool FlagHome { get; set; }
    }
}