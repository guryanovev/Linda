namespace Linda.Core
{
    using System;
    using System.Collections.Generic;

    using Linda.Core.Detecting;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    public class DefaultConfigManager : IConfigManager
    {
        private readonly IConfigLookup _configLookup;
        private readonly IYamlDeserializer _deserializer;
        private readonly IRootDetector _rootDetector;

        private IEnumerable<ConfigGroup> _configGroups;

        public DefaultConfigManager() : this(new FileBasedConfigLookup(), new CustomDeserializer(), new DefaultRootDetector())
        {
        }

        public DefaultConfigManager(IConfigLookup configLookup)
            : this(configLookup, new CustomDeserializer(), new DefaultRootDetector())
        {
        }

        public DefaultConfigManager(string configRoot) : this(new FileBasedConfigLookup(), new CustomDeserializer(), new ManualRootDetector(configRoot))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigManager"/> class. 
        /// </summary>
        /// <param name="configLookup"></param>
        /// <param name="deserializer"></param>
        /// <param name="rootDetector"></param>
        public DefaultConfigManager(IConfigLookup configLookup, IYamlDeserializer deserializer, IRootDetector rootDetector)
        {
            _configLookup = configLookup;
            _deserializer = deserializer;
            _rootDetector = rootDetector;
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

        public void WatchForConfig<TConfig>(Action<TConfig> callback)
        {
            throw new NotImplementedException();
        }
    }
}