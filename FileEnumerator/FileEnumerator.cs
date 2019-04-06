using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ERM_Models;

namespace FileUtilities
{
    public class FileEnumerator : IFileEnumerator
    {
        public FileEnumerator()
        {

        }

        public IEnumerable<string> GetFileNames(string filesPath, string filter)
        {
            var fileNames = Directory.EnumerateFiles(filesPath, filter)
            .Where(f => Path.GetFileNameWithoutExtension(f).StartsWith("lp_", StringComparison.CurrentCultureIgnoreCase) 
                || Path.GetFileNameWithoutExtension(f).StartsWith("tou_", StringComparison.CurrentCultureIgnoreCase));

            return fileNames.ToList();
        }
    }
}
