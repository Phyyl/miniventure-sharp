

using System.Collections.Generic;
using System.IO;

class IOHelper
{

    public static IEnumerable<FileInfo> GetFilesRecursive(string path, params string[] filters)
    {
        foreach (var filter in filters)
        {
            foreach (var file in Directory.GetFiles(path, filter))
            {
                yield return new FileInfo(file);
            }
        }

        foreach (var directory in Directory.GetDirectories(path))
        {
            foreach (var file in GetFilesRecursive(directory, filters))
            {
                yield return file;
            }
        }
    }
}