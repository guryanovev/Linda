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

            //var content = YamlFilesProvider.GetAllConfigContent(_configGroups);

            object resultConfig = new TConfig();

            foreach (var configGroup in _configGroups)
            {
                var content = ConfigContentProvider.GetConfigGroupContent(configGroup);

                var sourceGroup = new Deserializer().Deserialize(new StringReader(content));

                Converter(sourceGroup, ref resultConfig);
            }

            return (TConfig)resultConfig;
        }

        // TODO Перенести это в отдельный класс
        public void Converter(object source, ref object result)
        {
            if (!(source is Dictionary<object, object>))
            {
                result = Convert.ChangeType(source, result.GetType());
                return;
            }

            var sourceDictionary = source as Dictionary<object, object>;
            var destType = result.GetType();
            var properties = destType.GetProperties();

            foreach (var element in sourceDictionary)
            {
                try
                {
                    var pInfo = properties.Single(p => p.Name == (string) element.Key);

                    var propertyType = pInfo.PropertyType;

                    var tmpresult = propertyType != typeof (string) ? Activator.CreateInstance(propertyType) : string.Empty;

                    Converter(element.Value, ref tmpresult);

                    pInfo.SetValue(result, tmpresult, null);
                }
                catch (InvalidOperationException ex)
                {

                }
            }
        }
    }
}