using Glass.Mapper.Sc;
using Learn.Helix.Feature.NoticeBoard.Models;
using Learn.Helix.Feature.NoticeBoard.Repositories;
using Learn.Helix.Foundation.ORM.Controllers;
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

        public NoticeBoardController(INoticeboardRepository noticeboardRepository)
        {
            _noticeboardRepo = noticeboardRepository;
        }

        public ActionResult NoticeBoard()
        {
            var model = _noticeboardRepo.GetNoticeboardViewModel();
            return View(model);
        }
    }
}