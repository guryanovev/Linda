using System;
using System.Collections;
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
                _configGroups = _configSourceProvider.GetConfigGroups(_configRoot);
            }

            var content = YamlFilesProvider.GetAllConfigContent(_configGroups);

            var des = new Deserializer().Deserialize(new StringReader(content));

            var res = (TConfig)Converter((Dictionary<object, object>)des, typeof(TConfig));

            return res;
        }

        public object Converter(Dictionary<object, object> source, Type destType)
        {
            var result = Activator.CreateInstance(destType);

            var properties = destType.GetProperties();

            foreach (var element in source)
            {
                try
                {
                    PropertyInfo pInfo = properties.Single(p => p.Name == (string) element.Key);

                    var propertyType = pInfo.PropertyType;

                    object tmpresult;

                    //var value = Convert((Dictionary<object, object>)element.Value, type);

                    var elementType = element.Value.GetType();


                    if (!elementType.IsValueType && elementType.Name != "String")
                        tmpresult = Converter((Dictionary<object, object>) element.Value, propertyType);
                    else
                        tmpresult = Convert.ChangeType(element.Value, propertyType);

                    pInfo.SetValue(result, tmpresult, null);

                }
                catch (InvalidOperationException ex)
                {

                }
            }

            return result;
        }
    }
}