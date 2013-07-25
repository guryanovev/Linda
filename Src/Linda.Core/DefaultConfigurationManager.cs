namespace Linda.Core
{
    using Yaml_Parser;

    public class DefaultConfigurationManager : IConfigurationManager
    {
        private readonly string _configurationRoot;

        private readonly ConfigurationFolderContainer confCont;
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationManager"/> class. 
        /// </summary>
        /// <param name="configurationRoot">
        /// full path to config root directory
        /// </param>
        public DefaultConfigurationManager(string configurationRoot)
        {
            _configurationRoot = configurationRoot;
            confCont = this.LoadConfigurationFiles();
        }

        public TConfiguration GetConfiguration<TConfiguration>()
        {
            // todo insert implementation here
            throw new System.NotImplementedException();
        }

        private ConfigurationFolderContainer LoadConfigurationFiles()
        {
            return new ConfigurationFolderContainer(_configurationRoot);
        }
    }
}