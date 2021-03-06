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
    using Diagnostics;
    using Domain;
    using Rainbow.Model;
    using Serialization;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    public abstract class TemplatesResolverBase
    {
        public List<TemplateItem> Templates { get; private set; }

        protected TemplatesResolverBase(
            string serializationPath,
            string[] includePaths,
            string db = "master")
        {
            Assert.ArgumentNotNullOrEmpty(serializationPath, "serializationPath");

            var serializationFolder = new DirectoryInfo(serializationPath);

            Assert.IsTrue(serializationFolder.Exists, $"Path '{serializationPath}' does not exist");

            var syncItems = GetAllItems(serializationFolder, db, includePaths);

            InitializeTemplates(syncItems, serializationFolder);
        }

        protected TemplatesResolverBase(List<string> serializationPaths)
        {
            var syncItems = serializationPaths
                .Select(s => new FileInfo(s))
                .Where(s => s.Exists)
                .Select(s => GetItem(s, null))
                .ToList();

            InitializeTemplates(syncItems, null);
        }

        protected abstract List<SyncItem> GetAllItems(DirectoryInfo folder, string db, string[] includePaths);

        protected abstract SyncItem GetItem(FileInfo itemFile, Func<IItemData, bool> mustMatch);

        private void InitializeTemplates(List<SyncItem> syncItems, DirectoryInfo serializationFolder)
        {
            Templates = syncItems
                .Where(s => s.TemplateID == TemplateIDs.Template.ToString())
                .Select(t => new TemplateItem(t, syncItems))
                .ToList();

            Dictionary<string, TemplateItem> templateLookup = Templates.ToDictionary(t => t.SyncItem.ID, t => t);

            // resolve inheritance hierarchy
            foreach (var templateItem in Templates)
            {
                var baseTemplates = templateItem.SyncItem.SharedFields
                    .FirstOrDefault(f => f.FieldID == FieldIDs.BaseTemplate.ToString());

                if (!string.IsNullOrWhiteSpace(baseTemplates?.FieldValue))
                {
                    var baseTemplateIds = baseTemplates.FieldValue
                        .Split(new[] { '|', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(ID.IsID)
                        .Select(ID.Parse)
                        .ToArray();
                    var baseTemplateIdsInCurrentSet = baseTemplateIds
                        .Where(b => templateLookup.Keys.Contains(b.ToString()));
                    templateItem.BaseTemplates.AddRange(baseTemplateIdsInCurrentSet
                        .Select(b => templateLookup[b.ToString()]));

                    // resolve base templates outside of resolving set
                    if (serializationFolder != null)
                    {
                        foreach (ID baseTemplateId in baseTemplateIds
                            .Where(b => templateItem.BaseTemplates.All(bt => bt.SyncItem.ID != b.ToString())))
                        {
                            var baseTemplateFilePath = baseTemplateId.FindFilePath(serializationFolder);

                            if (string.IsNullOrWhiteSpace(baseTemplateFilePath))
                            {
                                continue;
                            }

                            var baseTemplateFile = new FileInfo(baseTemplateFilePath);

                            if (!baseTemplateFile.Exists)
                            {
                                continue;
                            }

                            var baseTemplateSyncItem =
                                SyncItem.ReadItem(new Tokenizer(baseTemplateFile.OpenText()));

                            if (baseTemplateSyncItem == null)
                            {
                                continue;
                            }

                            if (templateItem.BaseTemplates.All(b => b.SyncItem.ID != baseTemplateSyncItem.ID))
                            {
                                templateItem.BaseTemplates.Add(new TemplateItem(baseTemplateSyncItem,
                                    new List<SyncItem>()));
                            }
                        }
                    }
                }
            }
        }
    }
}
