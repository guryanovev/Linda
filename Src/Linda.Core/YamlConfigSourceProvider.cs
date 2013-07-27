namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;

    public class YamlConfigSourceProvider : IConfigSourceProvider
    {
        public IEnumerable<ConfigGroup> GetConfigGroups(string path, IFilesProvider yamlFilesProvider)
        {
            var newConfigGroups = new List<ConfigGroup>();

            var dir = new DirectoryInfo(path);

            while (dir != null)
            {
                if (Directory.Exists(Path.Combine(dir.FullName, "config")))
                {
                    var newConfigGroup = yamlFilesProvider.GetConfigGroupFromPath(Path.Combine(dir.FullName, "config"));

                    newConfigGroups.Add(newConfigGroup);
                }

                dir = dir.Parent;
            }

            newConfigGroups.Reverse();

            return newConfigGroups;
        }
    }
}
