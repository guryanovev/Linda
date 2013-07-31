namespace Linda.Core
{
    using System.Collections.Generic;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    public class DefaultConfigManager : IConfigManager
    {
        private readonly IConfigLookup _configLookup;

        private readonly string _configRoot;

        private readonly IYamlDeserializer _deserializer;

        private IEnumerable<ConfigGroup> _configGroups;

        public DefaultConfigManager(string configRoot) : this(configRoot, new DirectoryBasedConfigLookup(), new CustomDeserializer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigManager"/> class. 
        /// </summary>
        /// <param name="configRoot">
        /// full path to config root directory
        /// </param>
        /// <param name="configLookup"></param>
        /// <param name="deserializer"></param>
        public DefaultConfigManager(string configRoot, IConfigLookup configLookup, IYamlDeserializer deserializer)
        {
            _configLookup = configLookup;
            _deserializer = deserializer;
            _configRoot = configRoot;
        }

        public TConfig GetConfig<TConfig>() where TConfig : new()
        {
            if (_configGroups == null)
            {
                _configGroups = this._configLookup.GetConfigGroups(_configRoot);
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