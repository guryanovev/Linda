
namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public static class YamlFilesProvider
    {
        public static string GetConfigSourceContent(ConfigSource cs)
        {
            if (File.Exists(cs.Path))
            {
                using (var sr = new StreamReader(cs.Path))
                {
                    return sr.ReadToEnd();
                }
            }

            throw new FileNotFoundException();
        }

        public static string GetConfigGroupContent(ConfigGroup configGroup)
        {
            var folderContent = new StringBuilder();

            foreach (var configSource in configGroup)
            {
                folderContent.AppendLine(GetConfigSourceContent(configSource));
            }

            return folderContent.ToString();
        }

        public static string GetAllConfigContent(List<ConfigGroup> configGroups)
        {
            var folderContent = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                folderContent.AppendLine(GetConfigGroupContent(configGroup));
            }

            return folderContent.ToString();
        }
    }
}
