namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;

    class DefaultFilesSystem : IFilesSystem
    {
//        public ConfigGroup GetConfigGroupFromPath(string path)
//        {
//            var newConfigGroup = new ConfigGroup();
//
//            var yamlFiles = new DirectoryInfo(path).GetFiles("*.yml");
//
//            foreach (var yamlFile in yamlFiles)
//            {
//                newConfigGroup.AddConfigSource(new ConfigSource(yamlFile.FullName));
//            }
//
//            return newConfigGroup;
//        }

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
            return Directory.GetParent(path).FullName;
        }
    }
}
