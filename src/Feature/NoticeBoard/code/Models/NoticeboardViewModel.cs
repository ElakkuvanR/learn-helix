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

        public string PrimaryImageUrl { get; set; }

        public string SecondaryImageUrl { get; set; }

        public NoticeboardViewModel(INoticeboard noticeboard)
        {
            this.Author = noticeboard.Author;
            this.Quote = noticeboard.Quote;
            var item = Sitecore.Context.Database.GetItem(noticeboard.PrimaryImage?.MediaId.ToID());

            MediaItem mediaItem = null;
            if (item.Paths.IsMediaItem)
            {
                mediaItem = new MediaItem(item);
            }
            string mediaPrimaryUrl = MediaManager.GetMediaUrl(mediaItem,
                                new MediaUrlBuilderOptions()
                                {
                                    AllowStretch = false,
                                    Width = 500,
                                    Height = 300,
                                    Thumbnail = false,
                                    AbsolutePath = false,
                                    LowercaseUrls = true,
                                    DisableBrowserCache = true,
                                    DisableMediaCache = true,
                                });
            this.PrimaryImageUrl = Sitecore.StringUtil.EnsurePrefix('/', HashingUtils.ProtectAssetUrl(mediaPrimaryUrl));
            this.SecondaryImageUrl = noticeboard.SecondaryImage?.Src;
        }
    }
}