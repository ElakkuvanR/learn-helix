using System.Linq;
using AutoFixture;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Security.Accounts;
using Sitecore.Security.Domains;

namespace Learn.Helix.Foundation.Testing.Customizations
{
    public class BaseAuthenticationManagerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<BaseAuthenticationManager>(x =>
                x.FromFactory(CreateAuthenticationManager)
                    .OmitAutoProperties());
        }

        private static BaseAuthenticationManager CreateAuthenticationManager()
        {
            var authMgr = Substitute.For<BaseAuthenticationManager>();
            authMgr.When(manager => manager.SetActiveUser(Arg.Any<User>()))
                .Do(info => authMgr.GetActiveUser().Returns(info.Arg<User>()));

            authMgr.When(manager => manager.Logout())
                .Do(info =>
                {
                    var active = authMgr.GetActiveUser();
                    var anonymous = active?.Domain?.GetAnonymousUser() ?? new Domain("domain").GetAnonymousUser();
                    authMgr.GetActiveUser().Returns(anonymous);
                });
            authMgr.Login(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(info =>
                {
                    var user = User.FromName(info.ArgAt<string>(0), true);
                    authMgr.GetActiveUser().Returns(user);
                    return true;
                });


            return authMgr;
        }
    }
}