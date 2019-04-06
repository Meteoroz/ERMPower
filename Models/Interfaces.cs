using System;
using System.Collections.Generic;

namespace ERM_Models
{
    public interface ICsvProcessor
    {
        IEnumerable<string> ProcessFiles(IEnumerable<string> fileNames);
    }

    public interface IFileEnumerator
    {
        IEnumerable<string> GetFileNames(string filesPath, string filter);
    }
}
