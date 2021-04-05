using Learn.Helix.Feature.NoticeBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Feature.NoticeBoard.Repositories
{
    public interface INoticeboardRepository
    {
        NoticeboardViewModel GetNoticeboardViewModel();
    }
}