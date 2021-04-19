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
        public static Item AddItem(this Database db, string itemName, ID itemId, ID templateId)
        {
            var itemBase = new ItemDefinition(itemId, itemName, templateId, ID.Null, DateTime.Now);
            var templateBase = new ItemDefinition(templateId, itemName, TemplateIDs.Template, ID.Null, DateTime.Now);
            var itemActual = new ItemData(itemBase, Language.Parse("en"), Version.First, new FieldList());
            var templateActual = new ItemData(templateBase, Language.Parse("en"), Version.First, new FieldList());
            var item = Substitute.For<Item>(itemId, itemActual, db);
            var templateItem = Substitute.For<Item>(templateId, templateActual, db);
            item.Name.Returns(itemName);
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