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
            var filesSystem =
                Mock.Of<IFilesSystem>(
                    f =>
                    f.Exists("path") &&
                    f.GetFiles("path", "*.yml") == new List<string> { "path/config.yml" }
                    && f.GetFileContent("path/config.yml") == "Foo: fooValue"
                    && f.GetParentDirectory("path") == (string)null);

            var fileBasedConfigLookup = new FileBasedConfigLookup(filesSystem);

            var configGroups = fileBasedConfigLookup.GetConfigGroups("path").ToList();

            var content = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                content.Append(configGroup.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Foo: fooValue\r\n"));
        }

        [Test]
        public void SomeFileTest()
        {
            /*var filesSystem = new Mock<IFilesSystem>();

            filesSystem.Setup(fs => fs.Exists("path")).Returns(true);
            filesSystem.Setup(fs => fs.GetFiles("path")).Returns(() => new List<string> { "path/foo.yml", "path/bar.yml" });
            filesSystem.Setup(fs => fs.GetFileContent("path/foo.yml")).Returns("Foo: fooValue");
            filesSystem.Setup(fs => fs.GetFileContent("path/bar.yml")).Returns("Bar: barValue");
            filesSystem.Setup(fs => fs.GetParentDirectory("path")).Returns((string)null);*/

            var filesSystem =
                Mock.Of<IFilesSystem>(
                    f =>
                    f.Exists("path") 
                    && f.GetFiles("path", "*.yml") == new List<string> { "path/foo.yml", "path/bar.yml" }
                    && f.GetFileContent("path/foo.yml") == "Foo: fooValue"
                    && f.GetFileContent("path/bar.yml") == "Bar: barValue"
                    && f.GetParentDirectory("path") == (string)null);

            var fileBasedConfigLookup = new FileBasedConfigLookup(filesSystem);

            var configGroups = fileBasedConfigLookup.GetConfigGroups("path").ToList();

            var content = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                content.Append(configGroup.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Foo: fooValue\r\nBar: barValue\r\n"));
        }

        [Test]
        public void SomeFileTestWithHierarchy()
        {
            var filesSystem =
                Mock.Of<IFilesSystem>(
                    f =>
                    f.Exists("path") && f.Exists("under path") && f.Exists("under path")
                    && f.Exists("parent under path") && f.GetFiles("path", "*.yml") == new List<string> { "path/config1.yml" }
                    && f.GetFiles("under path", "*.yml")
                       == new List<string> { "under path/config2.yml", "under path/config3.yml" }
                    && f.GetFiles("parent under path", "*.yml") == new List<string> { "parent under path/config4.yml" }
                    && f.GetFileContent("path/config1.yml") == "Foo: fooValue"
                    && f.GetFileContent("under path/config2.yml") == "Bar: barValue"
                    && f.GetFileContent("under path/config3.yml") == "Baz: bazValue"
                    && f.GetFileContent("parent under path/config4.yml") == "FooBar: fooBarValue"
                    && f.GetParentDirectory("path") == "under path"
                    && f.GetParentDirectory("under path") == "parent under path"
                    && f.GetParentDirectory("parent under path") == (string)null);

            var fileBasedConfigLookup = new FileBasedConfigLookup(filesSystem);

            var configGroups = fileBasedConfigLookup.GetConfigGroups("path").ToList();

            var content = new StringBuilder();

            foreach (var configGroup in configGroups)
            {
                content.Append(configGroup.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("FooBar: fooBarValue\r\nBar: barValue\r\nBaz: bazValue\r\nFoo: fooValue\r\n"));
        }
    }
}
