using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Md.Infrastructure.Utils
{
    public static class IoHelper
    {
        public static string FileFromRelativePath(string fileName, string relativePath)
        {
            var currentAssemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;
            var currentAssemblyDirectory = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(currentAssemblyCodeBase).Path));
            var rootDirectory = Path.GetFullPath(Path.Combine(currentAssemblyDirectory, relativePath));
            return Path.Combine(rootDirectory, fileName);
        }
    }
}
