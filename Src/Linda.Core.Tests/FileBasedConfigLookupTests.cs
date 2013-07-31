namespace Linda.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Linda.Core.Lookup;
    using Moq;
    using NUnit.Framework;

    public class FileBasedConfigLookupTests
    {
        [Test]
        public void SimpleTest()
        {
            var filesSystem = new Mock<IFilesSystem>();

            filesSystem.Setup(f => f.Exists("path")).Returns(true);
            filesSystem.Setup(f => f.GetFiles("path")).Returns(() => new List<string> { "path/config.yml" });
            filesSystem.Setup(f => f.GetFileContent("path/config.yml")).Returns("Foo: fooValue");
            filesSystem.Setup(f => f.GetParentDirectory("path")).Returns((string)null);

            var fS = filesSystem.Object;

            var fileBasedConfigLookup = new FileBasedConfigLookup(fS);

            var configGroups = fileBasedConfigLookup.GetConfigGroups("path").ToList();

            var content = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                content.Append(configGroup.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Foo: fooValue\r\n"));
        }
    }
}
