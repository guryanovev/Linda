namespace Linda.Core
{
    using System.IO;

    class YamlFilesProvider : IFilesProvider
    {
        public ConfigGroup GetConfigGroupFromPath(string path)
        {
            var newConfigGroup = new ConfigGroup();

            var yamlFiles = new DirectoryInfo(path).GetFiles("*.yml");

            foreach (var yamlFile in yamlFiles)
            {
                newConfigGroup.AddConfigSource(new ConfigSource(yamlFile.FullName));
            }

            return newConfigGroup;
        }
    }
}
