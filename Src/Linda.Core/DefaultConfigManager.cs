using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using YamlDotNet.RepresentationModel;
using System.Linq;

namespace Linda.Core
{
    using AutoMapper;
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

        public TConfig GetConfig<TConfig>() where TConfig : new()
        {
            if (_configGroups == null)
            {
                _configGroups = _configSourceProvider.GetConfigGroups(_configRoot, new YamlFilesProvider());
            }

            var content = ConfigContentProvider.GetAllConfigContent(_configGroups);

            if (content == string.Empty)
            {
                return default(TConfig);
            }

            var resultConfig = new MyDeserializer().Deserialize<TConfig>(new StringReader(content));

            return resultConfig;
        }
    }
}