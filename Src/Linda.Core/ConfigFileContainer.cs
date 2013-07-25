namespace Linda.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ConfigFileContainer : IConfigSourceProvider
    {
        public List<ConfigGroup> GetConfigGroups(string path)
        {
            var newConfFolders = new List<ConfigGroup>();

            var dir = new DirectoryInfo(path);

            while (dir != null)
            {
                var cg = new ConfigGroup();

                if (Directory.Exists(path))
                {
                    var yamlFiles = Directory.GetFiles(path, "*.yaml");

                    foreach (var yamlFile in yamlFiles)
                    {
                        cg.AddConfigSource(new ConfigSource(yamlFile));
                    }

                    newConfFolders.Add(cg);
                }

                dir = dir.Parent;
            }

            newConfFolders.Reverse();

            return newConfFolders;
        }
    }
}
