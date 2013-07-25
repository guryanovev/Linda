namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using YamlDotNet.RepresentationModel.Serialization;

    public class DefaultConfigurationManager : IConfigurationManager
    {
        private readonly string _configurationRoot;

        private readonly List<ConfigGroup> confCont;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationManager"/> class. 
        /// </summary>
        /// <param name="configurationRoot">
        /// full path to config root directory
        /// </param>
        public DefaultConfigurationManager(string configurationRoot, IConfigSourceProvider csp)
        {
            _configurationRoot = configurationRoot;
           // confCont = this.LoadConfigurationFiles();
            confCont = csp.GetCs(configurationRoot);
        }

        public TConfiguration GetConfiguration<TConfiguration>()
        {
            confCont.ConfigFolders.Sort((cf1, cf2) => cf1.Path.Length - cf2.Path.Length);

            var bigStr = new StringBuilder();

            foreach (var file in confCont)
            {
                bigStr.AppendLine(file.Content);
            }

            return new Deserializer().Deserialize<TConfiguration>(new StringReader(bigStr.ToString()));

            
        }

        private ConfigurationFolderContainer LoadConfigurationFiles()
        {
            return new ConfigurationFolderContainer(_configurationRoot);
        }
    }
}