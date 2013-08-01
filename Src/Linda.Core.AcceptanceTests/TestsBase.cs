namespace Linda.Core.AcceptanceTests
{
    using System.IO;

    using Linda.Core.Detecting;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    using NUnit.Framework;

    [TestFixture]
    public abstract class TestsBase
    {
        private string _tempDirectory;

        [SetUp]
        public void SetUp()
        {
            _tempDirectory = Path.Combine(Directory.GetCurrentDirectory(), "tests_temp");
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_tempDirectory, true);
        }

        /// <summary>
        /// Loads configuration.
        /// </summary>
        /// <typeparam name="TConfig">type of configuration</typeparam>
        /// <param name="relativePath">relative path to configuration root directory</param>
        /// <returns>configuration object</returns>
        protected TConfig LoadConfig<TConfig>(string relativePath = null) where TConfig : new()
        {
            var manager = new DefaultConfigManager(new DirectoryBasedConfigLookup(), new CustomDeserializer(), new ManualRootDetector(GetFullPath(relativePath)));
            return manager.GetConfig<TConfig>();
        }

        /// <summary>
        /// Gets full path to temp file.
        /// </summary>
        /// <param name="relativePath">path to file or directory relatively to temp root</param>
        /// <returns>full path to file or directory</returns>
        protected string GetFullPath(string relativePath = null)
        {
            return string.IsNullOrEmpty(relativePath) ?
                _tempDirectory : 
                Path.Combine(_tempDirectory, relativePath);
        }

        /// <summary>
        /// Creates a file in temporary tests directory (will be deleted after tests execution).
        /// </summary>
        /// <param name="path">full path to file including file name</param>
        /// <param name="content">file content</param>
        protected void CreateFile(string path, string content)
        {
            var fullFilePath = GetFullPath(path);
            var fileDirectory = Path.GetDirectoryName(fullFilePath);
            if (fileDirectory != null && !Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            
            using (var writer = File.CreateText(fullFilePath))
            {
                writer.Write(content);
            }
        }
    }
}