using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPanit.Models;

namespace WebPanit.ViewModel
{
    public class HomeViewModel
    {
        public List<TradeMark> TradeMarks { get; set; }
        public List<Banner> Banners { get; set; }
        public List<LibraryImage> LibraryImages { get; set; }
        public List<FeedBack> FeedBacks { get; set; }
    }
}