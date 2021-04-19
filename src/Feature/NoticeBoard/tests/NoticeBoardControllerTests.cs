using FluentAssertions;
using Learn.Helix.Foundation.Testing.Customizations;
using Sitecore.Data.Items;
using Xunit;

namespace Learn.Helix.Feature.NoticeBoard.Test
{
    public class NoticeboardControllerTests
    {
        [Theory, DefaultAutoData]
        public void NoticeBoard_ShouldReturn_NoticeboardModel(string name)
        {
            name.Should().NotBeEmpty();
        }
    }
}
