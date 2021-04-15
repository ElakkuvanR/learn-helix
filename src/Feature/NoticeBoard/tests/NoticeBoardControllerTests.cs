using Microsoft.VisualStudio.TestTools.UnitTesting;
using Learn.Helix.Foundation.Testing.Customizations;
using System;
using Xunit;
using Sitecore.Data.Items;
using FluentAssertions;

namespace NoticeBoardControllerTests
{
    public class NoticeBoardControllerTests
    {
        [Theory, DefaultAutoData]
        public void NoticeBoard_ShouldReturn_ViewModel(Item item)
        {
            item.Should().NotBeNull();
        }
    }
}
