using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Feature.NoticeBoard.Models
{
    public class NoticeboardViewModel
    {
        public string Author { get; set; }

        public string Quote { get; set; }

        public string PrimaryImageUrl { get; set; }

        public string SecondaryImageUrl { get; set; }
        public NoticeboardViewModel(INoticeboard noticeboard)
        {
            this.Author = noticeboard.Author;
            this.Quote = noticeboard.Quote;
            this.PrimaryImageUrl = noticeboard.PrimaryImage?.Src;
            this.SecondaryImageUrl = noticeboard.SecondaryImage?.Src;
        }
    }
}