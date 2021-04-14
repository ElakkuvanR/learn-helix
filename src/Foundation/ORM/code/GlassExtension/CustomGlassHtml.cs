using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Learn.Helix.Foundation.ORM.GlassExtension
{
    public class CustomGlassHtml : GlassHtml, IGlassHtml
    {
        public CustomGlassHtml(ISitecoreService sitecoreService)
       : base(sitecoreService)
        {
        }

        public override string RenderImage<T>(T model, Expression<Func<T, object>> field, object parameters = null, bool isEditable = false, bool outputHeightWidth = false, string imgElementTemplate = "<img src={2}{0}{2} {1}/>")
        {
            return base.RenderImage(model, field, parameters, true, outputHeightWidth, imgElementTemplate);
        }

        public override string RenderImageUrl<T>(T model, Expression<Func<T, object>> field, object parameters = null)
        {
            return base.RenderImageUrl(model, field, parameters);
        }
    }
}