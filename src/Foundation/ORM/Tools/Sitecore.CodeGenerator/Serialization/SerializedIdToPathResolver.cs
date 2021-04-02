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

namespace Sitecore.CodeGenerator.Serialization
{
    using Sitecore.Data;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Find items by ID within serialized data using this class.
    /// It caches results, so as to make it perform well.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SerializedIdToPathResolver
    {
        private static readonly Dictionary<string, SerializedIdToPathSet> _pathSets
            = new Dictionary<string, SerializedIdToPathSet>();

        public static string FindFilePath(this ID id, DirectoryInfo serializationFolder)
        {
            lock (_pathSets)
            {
                // find or create the set of paths for the serialization folder
                SerializedIdToPathSet pathSet;

                if (_pathSets.ContainsKey(serializationFolder.FullName))
                {
                    pathSet = _pathSets[serializationFolder.FullName];
                }
                else
                {
                    pathSet = new SerializedIdToPathSet();
                    pathSet.FilePaths.Push(serializationFolder.FullName);
                    _pathSets.Add(serializationFolder.FullName, pathSet);
                }

                // get the individual item if already found
                if (pathSet.Paths.ContainsKey(id))
                {
                    return pathSet.Paths[id];
                }

                while (pathSet.FilePaths.Any())
                {
                    var filePath = pathSet.FilePaths.Pop();

                    foreach (var subdirectory in Directory.GetDirectories(filePath))
                    {
                        pathSet.FilePaths.Push(subdirectory);
                    }

                    string foundFile = null;

                    foreach (var file in Directory.GetFiles(filePath, "*.item"))
                    {
                        using (StreamReader sr = new StreamReader(file))
                        {
                            sr.ReadLine();
                            sr.ReadLine();

                            var itemIdStr = sr.ReadLine().Substring(4);

                            if (ID.IsID(itemIdStr))
                            {
                                var itemId = ID.Parse(itemIdStr);

                                if (!pathSet.Paths.ContainsKey(itemId))
                                {
                                    pathSet.Paths.Add(itemId, file);

                                    if (itemId == id)
                                    {
                                        foundFile = file;
                                    }
                                }
                            }
                        }
                    }
                    if (foundFile != null)
                    {
                        return foundFile;
                    }
                }
                return null;
            }
        }
    }
}
