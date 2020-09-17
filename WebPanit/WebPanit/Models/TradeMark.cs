using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanit.Models
{
    public class TradeMark
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tên danh mục gốc"), UIHint("TextBox")]
        [Required(ErrorMessage = "Hãy nhập tên cho danh mục gốc")]
        [StringLength(250)]
        public string Name { get; set; }

        [DisplayName("Ảnh đại diện/Logo")]
        [StringLength(250)]
        public string Avatar { get; set; }

        [Display(Name ="Hiển thị")]
        public bool Active { get; set; }

        [Display(Name = "Hiện trang chủ")]
        public bool FlagHome { get; set; }
        public List<TagProductCategory> ListTagProductCategory { get; set; }
        public TradeMark()
        {
            Active = true;
            FlagHome = true;
        }
    }
}
