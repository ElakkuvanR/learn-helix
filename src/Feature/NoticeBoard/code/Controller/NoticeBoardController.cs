using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Learn.Helix.Feature.NoticeBoard.Models;
using Learn.Helix.Feature.NoticeBoard.Repositories;
using Learn.Helix.Foundation.ORM.Controllers;
using Learn.Helix.Foundation.ORM.GlassExtension;
using Sitecore.XA.Foundation.Mvc.Abstraction;
using Sitecore.XA.Foundation.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn.Helix.Feature.NoticeBoard.Controller
{
    public class NoticeBoardController : GlassStandardController<INoticeboard>
    {
        private INoticeboardRepository _noticeboardRepo;

        private IMvcContext _mvcContext;

        public NoticeBoardController(INoticeboardRepository noticeboardRepository, IMvcContext mvcContext)
        {
            _noticeboardRepo = noticeboardRepository;
            _mvcContext = mvcContext;
        }

        public ActionResult NoticeBoard()
        {
            var model = _noticeboardRepo.GetNoticeboardViewModel();
            model.GlassModel = _mvcContext.GetDataSourceItem<INoticeboard>();
            return View(model);
        }
    }
}