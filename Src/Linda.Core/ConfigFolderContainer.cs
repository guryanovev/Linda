namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;

    public class ConfigFolderContainer : IConfigSourceProvider
    {
        public IEnumerable<ConfigGroup> GetConfigGroups(string path)
        {
            var newConfigFolders = new List<ConfigGroup>();

            var dir = new DirectoryInfo(path);

            while (dir != null)
            {
                var newConfigGroup = new ConfigGroup();

                if (Directory.Exists(Path.Combine(dir.FullName, "config")))
                {
                    var yamlFiles = Directory.GetFiles(Path.Combine(dir.FullName, "config"), "*.yml");

                    foreach (var yamlFile in yamlFiles)
                    {
                        newConfigGroup.AddConfigSource(new ConfigSource(yamlFile)); 
                    }

                    newConfigFolders.Add(newConfigGroup);
                }  

                dir = dir.Parent;
            }

            newConfigFolders.Reverse();

            return newConfigFolders;
        }
    }
}
