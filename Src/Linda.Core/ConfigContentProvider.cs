namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    // TODO изменить поадекватнее=)
    public static class ConfigContentProvider
    {
        public static string GetConfigSourceContent(ConfigSource configSource)
        {
            if (File.Exists(configSource.Path))
            {
                using (var sr = new StreamReader(configSource.Path))
                {
                    return sr.ReadToEnd();
                }
            }

            throw new FileNotFoundException();
        }

        public static string GetConfigGroupContent(ConfigGroup configGroup)
        {
            var configGroupContent = new StringBuilder();

            foreach (var configSource in configGroup)
            {
                configGroupContent.AppendLine(GetConfigSourceContent(configSource));
            }

            return configGroupContent.ToString();
        }

        public static string GetAllConfigContent(IEnumerable<ConfigGroup> configGroups)
        {
            var content = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                content.AppendLine(GetConfigGroupContent(configGroup));
            }

            return content.ToString();
        }
    }
}
