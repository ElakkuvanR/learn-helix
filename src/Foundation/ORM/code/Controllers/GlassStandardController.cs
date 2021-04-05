using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Foundation.ORM.Controllers
{
    public class GlassStandardController<T> : StandardController where T : class
    {
        public IMvcContext MvcContext { get; set; }

        public IRequestContext RequestContext { get; set; }

        public GlassStandardController()
        {

        }
        public GlassStandardController(IMvcContext mvcContext, IRequestContext requestContext)
        {
            MvcContext = mvcContext;
            RequestContext = requestContext;
        }
    }
}