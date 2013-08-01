namespace Linda.Core
{
    using System.Collections.Generic;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    public class DefaultConfigManager : IConfigManager
    {
        private readonly IConfigLookup _configLookup;
        private readonly IYamlDeserializer _deserializer;
        private readonly IRootDetector _rootDetector;

        private IEnumerable<ConfigGroup> _configGroups;

        public DefaultConfigManager(string configRoot) : this(configRoot, new FileBasedConfigLookup(), new CustomDeserializer(), rootDetector)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigManager"/> class. 
        /// </summary>
        /// <param name="configLookup"></param>
        /// <param name="deserializer"></param>
        public DefaultConfigManager(IConfigLookup configLookup, IYamlDeserializer deserializer, IRootDetector rootDetector)
        {
            _configLookup = configLookup;
            _deserializer = deserializer;
            _rootDetector = rootDetector;
            _configRoot = configRoot;
        }

        public TConfig GetConfig<TConfig>() where TConfig : new()
        {
            if (_configGroups == null)
            {
                var configRoot = _rootDetector.GetConfigRoot();
                _configGroups = _configLookup.GetConfigGroups(configRoot);
            }

            var content = string.Empty;
            foreach (var configGroup in _configGroups)
            {
                content += configGroup.RetrieveContent();
            }

            var resultConfig = _deserializer.Deserialize<TConfig>(content);

            return resultConfig;
        }
    }
}