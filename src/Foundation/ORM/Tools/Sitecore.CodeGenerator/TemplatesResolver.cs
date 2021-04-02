// /*
// Copyright (C) 2015 Robin Hermanussen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
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
    using Rainbow.Model;
    using Sitecore.Data.Serialization.ObjectModel;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    [ExcludeFromCodeCoverage]
    public class TemplatesResolver : TemplatesResolverBase
    {
        public TemplatesResolver(
            string serializationPath,
            string[] includePaths,
            string db = "master") : base(serializationPath, includePaths, db)
        {
        }

        protected override List<SyncItem> GetAllItems(DirectoryInfo folder, string db, string[] includePaths)
        {
            var result = new List<SyncItem>();

            foreach (var itemFile in folder.GetFiles("*.item", SearchOption.AllDirectories))
            {
                using (var sr = new StreamReader(itemFile.FullName))
                {
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();

                    var dbStr = sr.ReadLine()?.Substring(10);

                    if (dbStr != db)
                    {
                        continue;
                    }

                    var pathStr = sr.ReadLine()?.Substring(6);

                    if (!includePaths.Any(p => pathStr != null && pathStr.StartsWith(p)))
                    {
                        continue;
                    }
                }
                try
                {
                    result.Add(GetItem(itemFile, null));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to deserialize '{itemFile.FullName}'", ex);
                }
            }
            return result;
        }

        protected override SyncItem GetItem(FileInfo itemFile, Func<IItemData, bool> mustMatch)
        {
            return SyncItem.ReadItem(new Tokenizer(itemFile.OpenText()));
        }
    }
}
