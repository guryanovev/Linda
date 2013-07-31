﻿namespace Linda.Core.Tests
{
    using System.IO;
    using System.Text;

    using Linda.Core.Lookup;

    using Moq;

    using NUnit.Framework;

    public class DirectoryBasedConfigLookupTests
    {
        [Test]
        public void OneFileTest()
        {
            var fileSystemStub = new Mock<IFilesSystem>();

            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("this", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.GetFileContent("foo.yml")).Returns("Foo: FooValue");
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("this", "config"))).Returns(new string[1] { "foo.yml" });
            fileSystemStub.Setup(fs => fs.GetParentDirectory("this")).Returns((string)null);

            var fileSystem = fileSystemStub.Object;

            var dbcl = new DirectoryBasedConfigLookup(fileSystem);

            var cgs = dbcl.GetConfigGroups("this");

            var content = new StringBuilder();

            foreach (var cg in cgs)
            {
                content.AppendLine(cg.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Foo: FooValue\r\n\r\n"));
        }

        [Test]
        public void SomeFilesTest()
        {
            var fileSystemStub = new Mock<IFilesSystem>();

            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("this", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.GetFileContent("foo.yml")).Returns("Foo: FooValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("bar.yml")).Returns("Bar: BarValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("baz.yml")).Returns("Baz: BazValue");
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("this", "config"))).Returns(new string[3] { "foo.yml", "bar.yml", "baz.yml" });
            fileSystemStub.Setup(fs => fs.GetParentDirectory("this")).Returns((string)null);

            var fileSystem = fileSystemStub.Object;

            var dbcl = new DirectoryBasedConfigLookup(fileSystem);

            var cgs = dbcl.GetConfigGroups("this");

            var content = new StringBuilder();

            foreach (var cg in cgs)
            {
                content.AppendLine(cg.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Foo: FooValue\r\nBar: BarValue\r\nBaz: BazValue\r\n\r\n"));
        }

        [Test]
        public void HierarchyWithOneFilesTest()
        {
            var fileSystemStub = new Mock<IFilesSystem>();

            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("this", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("underground", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("core of the earth", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.GetFileContent("foo.yml")).Returns("Foo: FooValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("bar.yml")).Returns("Bar: BarValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("baz.yml")).Returns("Baz: BazValue");
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("this", "config"))).Returns(new string[1] { "foo.yml" });
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("underground", "config"))).Returns(new string[1] { "bar.yml" });
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("core of the earth", "config"))).Returns(new string[1] { "baz.yml" });
            fileSystemStub.Setup(fs => fs.GetParentDirectory("this")).Returns("underground");
            fileSystemStub.Setup(fs => fs.GetParentDirectory("underground")).Returns("core of the earth");
            fileSystemStub.Setup(fs => fs.GetParentDirectory("core of the earth")).Returns((string)null);

            var fileSystem = fileSystemStub.Object;

            var dbcl = new DirectoryBasedConfigLookup(fileSystem);

            var cgs = dbcl.GetConfigGroups("this");

            var content = new StringBuilder();

            foreach (var cg in cgs)
            {
                content.Append(cg.RetrieveContent());
            }

            var res = content.ToString();

            Assert.That(content.ToString(), Is.EqualTo("Baz: BazValue\r\nBar: BarValue\r\nFoo: FooValue\r\n"));
        }

        [Test]
        public void HierarchyWithSomeFilesTest()
        {
            var fileSystemStub = new Mock<IFilesSystem>();

            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("this", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("underground", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.Exists(Path.Combine("core of the earth", "config"))).Returns(true);
            fileSystemStub.Setup(fs => fs.GetFileContent("foo.yml")).Returns("Foo: FooValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("bar.yml")).Returns("Bar: BarValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("baz.yml")).Returns("Baz: BazValue");
            fileSystemStub.Setup(fs => fs.GetFileContent("foo2.yml")).Returns("Foo2: Foo2Value");
            fileSystemStub.Setup(fs => fs.GetFileContent("bar2.yml")).Returns("Bar2: Bar2Value");
            fileSystemStub.Setup(fs => fs.GetFileContent("baz2.yml")).Returns("Baz2: Baz2Value");
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("this", "config"))).Returns(new string[2] { "foo.yml", "foo2.yml" });
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("underground", "config"))).Returns(new string[2] { "bar.yml", "foo2.yml" });
            fileSystemStub.Setup(fs => fs.GetFiles(Path.Combine("core of the earth", "config"))).Returns(new string[2] { "baz.yml", "baz2.yml" });
            fileSystemStub.Setup(fs => fs.GetParentDirectory("this")).Returns("underground");
            fileSystemStub.Setup(fs => fs.GetParentDirectory("underground")).Returns("core of the earth");
            fileSystemStub.Setup(fs => fs.GetParentDirectory("core of the earth")).Returns((string)null);

            var fileSystem = fileSystemStub.Object;

            var dbcl = new DirectoryBasedConfigLookup(fileSystem);

            var cgs = dbcl.GetConfigGroups("this");

            var content = new StringBuilder();

            foreach (var cg in cgs)
            {
                content.Append(cg.RetrieveContent());
            }

            var res = content.ToString();

            Assert.That(content.ToString(), Is.EqualTo("Baz: BazValue\r\nBaz2: Baz2Value\r\nBar: BarValue\r\nFoo2: Foo2Value\r\nFoo: FooValue\r\nFoo2: Foo2Value\r\n"));
        }
    }
}