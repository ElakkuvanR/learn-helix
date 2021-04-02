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

    /// <summary>
    /// Holds data for found items within a serialization folder.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class SerializedIdToPathSet
    {
        internal Dictionary<ID, string> Paths { get; set; }

        internal Stack<string> FilePaths { get; set; }

        internal SerializedIdToPathSet()
        {
            Paths = new Dictionary<ID, string>();
            FilePaths = new Stack<string>();
        }
    }
}
