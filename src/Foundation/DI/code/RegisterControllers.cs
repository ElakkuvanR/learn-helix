using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Foundation.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Foundation.DI
{
    class RegisterControllers : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers("Learn*.Feature.*");
            serviceCollection.AddMvcControllers("Learn*.Foundation.*");
            serviceCollection.AddClassesWithServiceAttribute("Learn*.Feature.*");
            serviceCollection.AddClassesWithServiceAttribute("Learn*.Foundation.*");
        }
    }
}