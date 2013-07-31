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
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetFiles(string directory)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string path)
        {
            throw new System.NotImplementedException();
        }

        public string GetParentDirectory(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
