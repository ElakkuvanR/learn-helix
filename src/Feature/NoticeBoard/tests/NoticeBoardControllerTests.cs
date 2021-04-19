using FluentAssertions;
using Glass.Mapper.Sc.Web.Mvc;
using Learn.Helix.Feature.NoticeBoard.Controller;
using Learn.Helix.Feature.NoticeBoard.Models;
using Learn.Helix.Feature.NoticeBoard.Repositories;
using Learn.Helix.Foundation.Testing.Customizations;
using NSubstitute;
using Sitecore.Data.Items;
using System.Web.Mvc;
using Xunit;

namespace Learn.Helix.Feature.NoticeBoard.Test
{
    public class NoticeboardControllerTests
    {
        private readonly INoticeboardRepository _noticeboardRepo;

        private readonly INoticeboard _noticeboard;

        public NoticeboardControllerTests()
        {
            _noticeboardRepo = Substitute.For<INoticeboardRepository>();
            _noticeboard = Substitute.For<INoticeboard>();
        }
        [Fact]
        public void NoticeBoard_ShouldReturn_NoticeboardModel()
        {
            //Arrange
            var mvcContext = Substitute.For<IMvcContext>();
            var noticeBoardController = new NoticeBoardController(_noticeboardRepo, mvcContext);
            _noticeboard.PrimaryImage.Returns(new Glass.Mapper.Sc.Fields.Image());
            _noticeboard.SecondaryImage.Returns(new Glass.Mapper.Sc.Fields.Image());
            NoticeboardViewModel model = new NoticeboardViewModel(_noticeboard);
           
            // Act
            _noticeboardRepo.GetNoticeboardViewModel().Returns(model);

            //Assert
            noticeBoardController.NoticeBoard().Should().BeOfType<ViewResult>();
        }
    }
}
