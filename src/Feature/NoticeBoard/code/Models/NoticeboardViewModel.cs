using Glass.Mapper.Sc.Fields;
using Learn.Helix.Foundation.ORM.Models;
using Sitecore.Common;
using Sitecore.Data.Items;
using Sitecore.Links.UrlBuilders;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Feature.NoticeBoard.Models
{
    public class NoticeboardViewModel : RenderingGlassModel<INoticeboard>
    {
        public string Author { get; set; }

        public string Quote { get; set; }

        public Image PrimaryImage { get; set; }

        public Image SecondaryImage { get; set; }

        public NoticeboardViewModel(INoticeboard noticeboard)
        {
            this.Author = noticeboard.Author;
            this.Quote = noticeboard.Quote;
            this.PrimaryImage = noticeboard.PrimaryImage;
            this.SecondaryImage = noticeboard.SecondaryImage;
        }
    }
}