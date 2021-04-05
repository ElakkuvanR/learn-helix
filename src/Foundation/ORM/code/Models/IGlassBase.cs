using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Globalization;
using System;
using Sitecore.Data.Items;

namespace Learn.Helix.Foundation.ORM.Models
{
    public partial interface IGlassBase
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [SitecoreId]
        Guid Id { get; }

        [SitecoreItem]
        Item Item { get; set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Language)]
        Language Language { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; }

        /// <summary>
        /// Gets the name of the template.
        /// </summary>
        /// <value>
        /// The name of the template.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.TemplateName)]
        string TemplateName { get; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        Guid TemplateId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [SitecoreInfo(SitecoreInfoType.Name)]
        string ItemName { get; }

        bool NotEditable { get; set; }
    }
}