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
using Sitecore.Data.Fields;
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
            item.Template.Returns(new TemplateItem(templateItem));
            item.Fields.Returns(Substitute.For<FieldCollection>(item));
            item.Statistics.Returns(Substitute.For<ItemStatistics>(item));
            db.GetItem(item.ID).Returns(item);
            return item;
        }

        public static void AddField(this Item item, string fieldName, string fieldValue, ID fieldId)
        {
            var field = Substitute.For<Field>(fieldId, item);
            field.Name.Returns(fieldName);
            field.Value.Returns(fieldValue);
            item.Fields[fieldId].Returns(field);
            item.Fields[fieldName].Returns(field);
        }
    }
}