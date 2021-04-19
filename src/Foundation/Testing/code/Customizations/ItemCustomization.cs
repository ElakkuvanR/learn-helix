using AutoFixture;
using Sitecore.Data;
using Sitecore.Data.Items;
using Learn.Helix.Foundation.Testing.Extension;

namespace Learn.Helix.Foundation.Testing.Customizations
{
    public class ItemCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Item>(x =>
                x.FromFactory(() => CreateItem(fixture))
                    .OmitAutoProperties()
            );
        }

        private static Item CreateItem(IFixture fixture)
        {
            var database = fixture.Create<Database>();
            var itemId = fixture.Create<ID>();
            var templateId = fixture.Create<ID>();
            return database.AddItem(itemId, templateId, fixture.Create<string>());
        }
    }
}