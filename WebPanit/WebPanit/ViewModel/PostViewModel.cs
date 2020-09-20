using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPanit.Models;

namespace WebPanit.ViewModel
{
    public class ListPostViewModel
    {
        public List<Post> Posts { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}