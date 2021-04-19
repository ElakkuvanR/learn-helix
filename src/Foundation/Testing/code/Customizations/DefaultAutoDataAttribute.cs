using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Foundation.Testing.Customizations
{
    public class DefaultAutoDataAttribute : AutoDataAttribute
    {
        public DefaultAutoDataAttribute()
            : base(() => new global::AutoFixture.Fixture()
                .Customize(new DatabaseCustomization())
                .Customize(new ItemCustomization())
                .Customize(new BaseAuthenticationManagerCustomization()))
        {
        }
    }
}