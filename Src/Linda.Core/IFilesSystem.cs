namespace Linda.Core
{
    using System.Collections.Generic;

    public interface IFilesSystem
    {
        string GetFileContent(string path);

        IEnumerable<string> GetFiles(string directory, string searchPattern);

        bool Exists(string path);

        string GetParentDirectory(string path);
    }
}
