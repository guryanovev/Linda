namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;

    public class DefaultFilesSystem : IFilesSystem
    {
        public string GetFileContent(string path)
        {
            return File.ReadAllText(path);
        }

        public IEnumerable<string> GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string GetParentDirectory(string path)
        {
            return Directory.GetParent(path) == null ? null : Directory.GetParent(path).FullName;
        }
    }
}
