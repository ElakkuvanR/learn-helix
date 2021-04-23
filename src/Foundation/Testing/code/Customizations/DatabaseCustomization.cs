using AutoFixture;
using NSubstitute;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Foundation.Testing.Customizations
{
    public class DatabaseCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Database>(x =>
                x.FromFactory(() =>
                {
                    var customDatabase = Substitute.For<Database>();
                    customDatabase.Name.Returns("master");
                    customDatabase.Items.Returns(Substitute.For<ItemRecords>(customDatabase));
                    return customDatabase;
                })
                    .OmitAutoProperties());
        }
    }
}