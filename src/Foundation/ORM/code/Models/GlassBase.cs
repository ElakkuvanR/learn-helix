using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Helix.Foundation.ORM.Models
{
    public class GlassBase : IGlassBase
    {
        public Guid Id { get; }

        public Item Item { get; set; }

        public Language Language { get; }

        public int Version { get; }

        public string TemplateName { get; }

        public Guid TemplateId { get; }

        public string ItemName { get; }

        public bool NotEditable { get; set; }
    }
}