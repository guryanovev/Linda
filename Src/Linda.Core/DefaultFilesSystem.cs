namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public class DefaultFilesSystem : IFilesSystem
    {
        public string GetFileContent(string path)
        {
            return File.ReadAllText(path);
        }

        public IEnumerable<string> GetFiles(string directory, string searchPatternRegEx)
        {
            var files = new DirectoryInfo(directory).GetFiles();

            var result = new List<string>();

            foreach (var file in files)
            {
                var regex = new Regex(searchPatternRegEx);

                if (!regex.IsMatch(file.Name))
                {
                    continue;
                }

                var match = regex.Match(file.Name);

                if (match.Length == file.Name.Length)
                {
                    result.Add(match.ToString());
                }
            }

            return result;
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
