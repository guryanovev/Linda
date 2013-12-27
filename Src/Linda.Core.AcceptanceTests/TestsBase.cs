namespace Linda.Core.AcceptanceTests
{
    using System.IO;
    using System.Threading;

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
            Thread.Sleep(200);
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
            using (var manager = CreateConfigManager(new DirectoryBasedConfigLookup(), relativePath))
            {
                return manager.GetConfig<TConfig>();
            }
        }

        /// <summary>
        /// Creates default instance of <see cref="IConfigManager"/>.
        /// </summary>
        /// <param name="relativePath">path to configuration</param>
        /// <returns>configuration manager instance</returns>
        protected DefaultConfigManager CreateConfigManager(string relativePath = null)
        {
            return CreateConfigManager(new DirectoryBasedConfigLookup(), relativePath);
        }

        /// <summary>
        /// Creates default instance of <see cref="IConfigManager"/>.
        /// </summary>
        /// <param name="lookup">config lookup</param>
        /// <param name="relativePath">path to configuration</param>
        /// <returns>configuration manager instance</returns>
        protected DefaultConfigManager CreateConfigManager(IConfigLookup lookup, string relativePath = null)
        {
            return new DefaultConfigManager(
                lookup, 
                new CustomDeserializer(),
                new ManualRootDetector(GetFullPath(relativePath)));
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

        /// <summary>
        /// Removes temporary file.
        /// </summary>
        /// <param name="path"></param>
        protected void DeleteFileOrDirectory(string path)
        {
            var fullFilePath = GetFullPath(path);
            
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            } else if (Directory.Exists(fullFilePath))
            {
                Directory.Delete(fullFilePath, true);
            }
        }

        /// <summary>
        /// Updates existing file content.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        protected void UpdateFile(string path, string content)
        {
            CreateFile(path, content);
        }
    }
}