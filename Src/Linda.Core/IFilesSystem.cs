namespace Linda.Core
{
    using System.Collections.Generic;

    public interface IFilesSystem
    {
        //ConfigGroup GetConfigGroupFromPath(string path);

        string GetFileContent(string path);

        IEnumerable<string> GetFiles(string directory);

        bool Exists(string path);

        string GetParentDirectory(string path);
    }
}
