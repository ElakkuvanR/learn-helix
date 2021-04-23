using AutoFixture.Xunit2;
using FluentAssertions;
using Glass.Mapper.Sc.Web.Mvc;
using Learn.Helix.Feature.NoticeBoard.Repositories;
using Learn.Helix.Foundation.Testing.Customizations;
using Learn.Helix.Foundation.Testing.Extension;
using NSubstitute;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Xunit;

namespace Learn.Helix.Feature.NoticeBoard.Test
{
    public class NoticeBoardRepositoryTests
    {
        private readonly IMvcContext _mvcContext;


        public NoticeBoardRepositoryTests()
        {
            _mvcContext = Substitute.For<IMvcContext>();
        }

        [Theory, DefaultAutoData]
        public void GetField_ShouldReturn_FieldValue(Item item, string fieldValue, ID fieldId)
        {
            // Arrange 
            item.AddField("textField", fieldValue, fieldId);
            var noticeBoardRepo = new NoticeboardRepository(_mvcContext);
            // Act
            string returnedValue = noticeBoardRepo.GetFieldValue(item, "textField");
            // Assert
            returnedValue.Should().BeEquivalentTo(fieldValue);
        }
    }
}
