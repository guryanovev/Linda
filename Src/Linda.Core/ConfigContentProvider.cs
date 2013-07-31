namespace Linda.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    [Obsolete]
    public class ConfigContentProvider
    {
        public string GetConfigSourceContent(ConfigSource configSource)
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

        public string GetConfigGroupContent(ConfigGroup configGroup)
        {
            var configGroupContent = new StringBuilder();

            foreach (var configSource in configGroup)
            {
                configGroupContent.AppendLine(GetConfigSourceContent(configSource));
            }

            return configGroupContent.ToString();
        }

        public string GetAllConfigContent(IEnumerable<ConfigGroup> configGroups)
        {
            var content = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                content.Append(GetConfigGroupContent(configGroup));
            }

            return content.ToString();
        }
    }
}
