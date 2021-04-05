using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Glass.Mapper.Sc.Web.WebForms;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.XA.Foundation.Mvc.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Foundation.ORM.DI
{
    public class RegisterGlassContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            var services = new[]
            {
                ServiceDescriptor.Describe(typeof(ISitecoreService), provider => new SitecoreService(Sitecore.Context.Database), ServiceLifetime.Transient),
                ServiceDescriptor.Describe(typeof(IRequestContext), typeof(RequestContext), ServiceLifetime.Transient),
                ServiceDescriptor.Describe(typeof(IMvcContext), typeof(MvcContext), ServiceLifetime.Transient)
            };

            foreach (var serviceDescriptor in services)
            {
                serviceCollection.Add(serviceDescriptor);
            }
        }
    }
}