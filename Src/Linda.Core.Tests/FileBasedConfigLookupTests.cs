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

        [Test]
        public void SomeFileTest()
        {
            var filesSystem = new Mock<IFilesSystem>();

            filesSystem.Setup(fs => fs.Exists("path")).Returns(true);
            filesSystem.Setup(fs => fs.GetFiles("path")).Returns(() => new List<string> { "path/foo.yml", "path/bar.yml" });
            filesSystem.Setup(fs => fs.GetFileContent("path/foo.yml")).Returns("Foo: fooValue");
            filesSystem.Setup(fs => fs.GetFileContent("path/bar.yml")).Returns("Bar: barValue");
            filesSystem.Setup(fs => fs.GetParentDirectory("path")).Returns((string)null);

            var fS = filesSystem.Object;

            var fileBasedConfigLookup = new FileBasedConfigLookup(fS);

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
            var filesSystem = new Mock<IFilesSystem>();

            filesSystem.Setup(fs => fs.Exists("path")).Returns(true);
            filesSystem.Setup(fs => fs.Exists("under path")).Returns(true);
            filesSystem.Setup(fs => fs.Exists("parent under path")).Returns(true);

            filesSystem.Setup(fs => fs.GetFiles("path")).Returns(() => new List<string> { "path/config1.yml" });
            filesSystem.Setup(fs => fs.GetFiles("under path"))
                       .Returns(() => new List<string> { "under path/config2.yml", "under path/config3.yml" });
            filesSystem.Setup(fs => fs.GetFiles("parent under path"))
                       .Returns(() => new List<string> { "parent under path/config4.yml" });

            filesSystem.Setup(fs => fs.GetFileContent("path/config1.yml")).Returns("Foo: fooValue");
            filesSystem.Setup(fs => fs.GetFileContent("under path/config2.yml")).Returns("Bar: barValue");
            filesSystem.Setup(fs => fs.GetFileContent("under path/config3.yml")).Returns("Baz: bazValue");
            filesSystem.Setup(fs => fs.GetFileContent("parent under path/config4.yml")).Returns("FooBar: fooBarValue");

            filesSystem.Setup(fs => fs.GetParentDirectory("path")).Returns(() => "under path");
            filesSystem.Setup(fs => fs.GetParentDirectory("under path")).Returns(() => "parent under path");
            filesSystem.Setup(fs => fs.GetParentDirectory("parent under path")).Returns((string)null);

            var fS = filesSystem.Object;

            var fileBasedConfigLookup = new FileBasedConfigLookup(fS);

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
