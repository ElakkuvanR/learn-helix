// <autogenerated>
//   This file was generated by T4 code generator Templates.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace Learn.Helix.Feature.NoticeBoard.Models
{
  using System;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  
  using global::Glass.Mapper.Sc.Configuration;
  using global::Glass.Mapper.Sc.Configuration.Attributes;
  using global::Glass.Mapper.Sc.Fields;
    
  using Sitecore.Data;
  using Learn.Helix.Foundation.ORM.Models;
  /// <summary>
  /// Represents const IDs for mapped type.
  /// </summary>
  public partial struct Templates
  {
   #region Template Ids
    public struct Noticeboard
    {
      public const string Id = "{4138C03D-8214-4481-94B4-5E451E1B42E8}";
      public static ID TemplateId { get; } = new ID(Id);

      public struct Fields
      {
        public const string PrimaryImage = "{EA140418-2423-4B00-B484-2171DCA8D942}";
        public const string SecondaryImage = "{9DB1C41A-4FFD-43FC-B96A-C10412218006}";
        public const string Author = "{A4197F8A-F13F-4DF3-BE2D-5E7999E44CDF}";
        public const string Quote = "{B4060B13-6D50-4FC8-A9CA-2C60329DA9DB}";
      }

      public struct FieldIds
      {
        public static ID PrimaryImage { get; } = new ID(Fields.PrimaryImage);
        public static ID SecondaryImage { get; } = new ID(Fields.SecondaryImage);
        public static ID Author { get; } = new ID(Fields.Author);
        public static ID Quote { get; } = new ID(Fields.Quote);
      }
    }
    #endregion
  }

  /// <summary>
  /// Represents a mapped type for item {4138C03D-8214-4481-94B4-5E451E1B42E8} in Sitecore.
  /// Path: /sitecore/templates/Feature/NoticeBoard/_Noticeboard
  /// </summary>
  [SitecoreType(TemplateId = Templates.Noticeboard.Id, EnforceTemplate = SitecoreEnforceTemplate.TemplateAndBase)]
  public partial interface INoticeboard : IGlassBase
  {
    #region Images

    [SitecoreField(FieldId = Templates.Noticeboard.Fields.PrimaryImage)]
    Image PrimaryImage { get; set; }

    [SitecoreField(FieldId = Templates.Noticeboard.Fields.SecondaryImage)]
    Image SecondaryImage { get; set; }

	#endregion

    #region Notice Info

    [SitecoreField(FieldId = Templates.Noticeboard.Fields.Author)]
    string Author { get; set; }

    [SitecoreField(FieldId = Templates.Noticeboard.Fields.Quote)]
    string Quote { get; set; }

	#endregion

  }
}
