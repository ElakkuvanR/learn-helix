using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NSubstitute;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Version = Sitecore.Data.Version;

namespace Learn.Helix.Foundation.Testing.Extension
{
    public static class ItemTestExtension
    {
        public static Item AddItem(this Database db, ID itemId, ID templateId, string name)
        {
            var itemDefintition = new ItemDefinition(itemId, name, templateId, ID.Null, DateTime.Now);
            var templateDefintition = new ItemDefinition(templateId, name, TemplateIDs.Template, ID.Null, DateTime.Now);
            var itemData = new ItemData(itemDefintition, Language.Parse("en"), Version.First, new FieldList());
            var templateItemData = new ItemData(templateDefintition, Language.Parse("en"), Version.First, new FieldList());
            var item = Substitute.For<Item>(itemId, itemData, db);
            var templateItem = Substitute.For<Item>(templateId, templateItemData, db);
            item.Name.Returns(name);
            item.Versions.Returns(Substitute.For<ItemVersions>(item));
            item.Versions.Count.Returns(1);
            item.Paths.Returns(Substitute.For<ItemPath>(item));
            item.Paths.Path.Returns(item.Name);
            item.Language.Returns(Language.Parse("en"));
            item.Languages.Returns(new Language[] { Language.Parse("en") });
            item.Template.Returns(new TemplateItem(templateItem));
            item.Fields.Returns(Substitute.For<FieldCollection>(item));
            item.Statistics.Returns(Substitute.For<ItemStatistics>(item));
            db.GetItem(item.ID).Returns(item);
            db.GetItem(item.Paths.Path).Returns(item);
            item.Axes.Returns(Substitute.For<ItemAxes>(item));
            return item;
        }
    }
}