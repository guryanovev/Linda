﻿namespace Linda.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ConfigFolderContainer : IConfigSourceProvider
    {
        public List<ConfigGroup> GetCs(string path)
        {
            var newConfFolders = new List<ConfigGroup>();

            var dir = new DirectoryInfo(path);

            while (dir != null)
            {
                var cg = new ConfigGroup();

                if (Directory.Exists(dir.FullName + "/config/"))
                {
                    var yamlFiles = dir.GetFiles("*.yaml");

                    foreach (var yamlFile in yamlFiles)
                    {
                        cg.AddConfigSource(new ConfigSource(yamlFile.FullName));
                    }
                }

                newConfFolders.Add(cg);

                dir = dir.Parent;
            }

            return newConfFolders;
        }
    }
}