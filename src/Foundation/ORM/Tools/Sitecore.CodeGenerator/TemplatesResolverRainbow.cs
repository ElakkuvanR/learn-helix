// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program. If not, see http://www.gnu.org/licenses/.
// */

namespace Sitecore.CodeGenerator
{
    using Data;
    using Data.Serialization.ObjectModel;
    using Rainbow.Model;
    using Rainbow.Storage.Yaml;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    public class TemplatesResolverRainbow : TemplatesResolverBase
    {
        public TemplatesResolverRainbow(string serializationPath, string[] includePaths, string db = "master")
            : base(serializationPath, includePaths, db)
        {
        }

        public TemplatesResolverRainbow(List<string> serializationPaths) : base(serializationPaths)
        {
        }

        protected override List<SyncItem> GetAllItems(DirectoryInfo folder, string db, string[] includePaths)
        {
            return folder.GetFiles("*.yml", SearchOption.AllDirectories).Select(itemFile => GetItem(itemFile, i => includePaths.Any(p => i.Path.StartsWith(p)))).Where(syncItem => syncItem != null).ToList();
        }

        protected override SyncItem GetItem(FileInfo itemFile, Func<IItemData, bool> mustMatch)
        {
            var formatter = new YamlSerializationFormatter(null, null);

            SyncItem syncItem = null;

            using (var sr = new StreamReader(itemFile.FullName))
            {
                try
                {
                    var item = formatter.ReadSerializedItem(sr.BaseStream, itemFile.Name);

                    if (item == null || (mustMatch != null && !mustMatch(item)))
                    {
                        return null;
                    }

                    syncItem = new SyncItem()
                    {
                        Name = item.Name,
                        BranchId = new ID(item.BranchId).ToString(),
                        DatabaseName = item.DatabaseName,
                        ID = new ID(item.Id).ToString(),
                        ItemPath = item.Path,
                        TemplateID = new ID(item.TemplateId).ToString(),
                        ParentID = new ID(item.ParentId).ToString()
                    };

                    foreach (var version in item.Versions)
                    {
                        var syncVersion = syncItem.AddVersion(version.Language.Name, version.VersionNumber.ToString(),
                            version.VersionNumber.ToString());
                        foreach (var field in version.Fields)
                        {
                            syncVersion.AddField(new ID(field.FieldId).ToString(), field.NameHint, field.NameHint, field.Value, true);
                        }
                    }

                    foreach (var sharedField in item.SharedFields)
                    {
                        syncItem.AddSharedField(new ID(sharedField.FieldId).ToString(), sharedField.NameHint,
                            sharedField.NameHint, sharedField.Value, true);
                    }

                    foreach (var unversionedField in item.UnversionedFields)
                    {
                        foreach (var version in syncItem.Versions.Where(v => v.Language == unversionedField.Language.Name))
                        {
                            foreach (var itemFieldValue in unversionedField.Fields)
                            {
                                version.AddField(new ID(itemFieldValue.FieldId).ToString(), itemFieldValue.NameHint,
                                    itemFieldValue.NameHint, itemFieldValue.Value, true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to deserialize '{itemFile.FullName}'", ex);
                }
            }
            return syncItem;
        }
    }
}
