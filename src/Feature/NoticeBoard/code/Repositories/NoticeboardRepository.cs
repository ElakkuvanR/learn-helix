using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Learn.Helix.Feature.NoticeBoard.Models;
using Sitecore.Foundation.DependencyInjection;
using Sitecore.Mvc.Presentation;

namespace Learn.Helix.Feature.NoticeBoard.Repositories
{
    [Service(typeof(INoticeboardRepository))]
    public class NoticeboardRepository : INoticeboardRepository
    {
        private readonly IMvcContext _mvcContext;

        public NoticeboardRepository(IMvcContext mvcContext)
        {
            _mvcContext = mvcContext;
        }
        public NoticeboardViewModel GetNoticeboardViewModel()
        {
            var model = _mvcContext.GetDataSourceItem<INoticeboard>();

            return new NoticeboardViewModel(model);
        }
    }
}