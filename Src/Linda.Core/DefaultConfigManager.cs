﻿namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;
    using YamlDotNet.RepresentationModel.Serialization;

    public class DefaultConfigManager : IConfigManager
    {
        private readonly IConfigSourceProvider _configSourceProvider;

        private readonly string _configRoot;

        private IEnumerable<ConfigGroup> _configGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigManager"/> class. 
        /// </summary>
        /// <param name="configRoot">
        /// full path to config root directory
        /// </param>
        /// <param name="configSourceProvider"></param>
        public DefaultConfigManager(string configRoot, IConfigSourceProvider configSourceProvider)
        {
            _configSourceProvider = configSourceProvider;
            _configRoot = configRoot;
        }

        public TConfig GetConfig<TConfig>()
        {
            if (_configGroups == null)
            {
                _configGroups = _configSourceProvider.GetConfigGroups(_configRoot);
            }

            var content = YamlFilesProvider.GetAllConfigContent(_configGroups);

            return new Deserializer().Deserialize<TConfig>(new StringReader(content)); 
        }
    }
}