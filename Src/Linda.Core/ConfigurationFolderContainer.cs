using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Linda.Core
{
    using System.Text;

    public class ConfigurationFolder
    {
        public ConfigurationFolder(string fileContent, string path)
        {
            this.Content = fileContent;
            this.Path = path;
        }

        public readonly string Content;

        public readonly string Path;
    }

    class ConfigurationFolderContainer : IEnumerable<ConfigurationFolder>, IConfigSourceProvider
    {
        public readonly List<ConfigurationFolder> ConfigFolders;

        public ConfigurationFolderContainer(string path)
        {
            this.ConfigFolders = this.GetCs(path);
        }

        /*private List<ConfigurationFolder> LoadYamlFiles(string path)
        {
            var newConfFolders = new List<ConfigurationFolder>(); 

            var dir = new DirectoryInfo(path);

            var level = -1;

            while (dir != null)
            {
                level++;

                if (Directory.Exists(dir.FullName + "/config/"))
                {
                    newConfFolders.Add(new ConfigurationFolder(this.GetFolderConfigContent(dir.FullName + "/config/"), dir.FullName));
                }

                dir = dir.Parent;
            }

            return newConfFolders;
        }*/

        private static string GetFileContent(string path)
        {
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }

            throw new FileNotFoundException();
        }

        private string GetFolderConfigContent(string path)
        {
            var files = Directory.GetFiles(path, "*.yaml");

            var folderContent = new StringBuilder();

            foreach (var file in files)
            {
                folderContent.AppendLine(GetFileContent(file));
            }

            return folderContent.ToString();
        }

        public IEnumerator<ConfigurationFolder> GetEnumerator()
        {
            return this.ConfigFolders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<ConfigurationFolder> GetCs(string path)
        {
            var newConfFolders = new List<ConfigurationFolder>();

            var dir = new DirectoryInfo(path);

            var level = -1;

            while (dir != null)
            {
                level++;

                if (Directory.Exists(dir.FullName + "/config/"))
                {
                    newConfFolders.Add(new ConfigurationFolder(this.GetFolderConfigContent(dir.FullName + "/config/"), dir.FullName));
                }

                dir = dir.Parent;
            }

            return newConfFolders;
        }
    }
}
