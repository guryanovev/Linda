namespace Linda.Core.Tests
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
            var fileSystem =
                Mock.Of<IFilesSystem>(
                    fs =>
                    fs.Exists(Path.Combine("this", "config")) && fs.GetFileContent("foo.yml") == "Foo: FooValue"
                    && fs.GetFiles(Path.Combine("this", "config")) == new string[1] { "foo.yml" }
                    && fs.GetParentDirectory("this") == (string)null);

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
            var fileSystem =
                Mock.Of<IFilesSystem>(
                    fs =>
                    fs.Exists(Path.Combine("this", "config")) && fs.GetFileContent("foo.yml") == "Foo: FooValue"
                    && fs.GetFileContent("bar.yml") == "Bar: BarValue"
                    && fs.GetFileContent("baz.yml") == "Baz: BazValue"
                    && fs.GetFiles(Path.Combine("this", "config")) == new string[3] { "foo.yml", "bar.yml", "baz.yml" }
                    && fs.GetParentDirectory("this") == (string)null);

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
            var fileSystem =
                Mock.Of<IFilesSystem>(
                    fs =>
                    fs.Exists(Path.Combine("this", "config"))
                    && fs.Exists(Path.Combine("underground", "config"))
                    && fs.Exists(Path.Combine("core of the earth", "config"))
                    && fs.GetFileContent("foo.yml") == "Foo: FooValue" && fs.GetFileContent("bar.yml") == "Bar: BarValue"
                    && fs.GetFileContent("baz.yml") == "Baz: BazValue"
                    && fs.GetFiles(Path.Combine("this", "config")) == new string[1] { "foo.yml" }
                    && fs.GetFiles(Path.Combine("underground", "config")) == new string[1] { "bar.yml" }
                    && fs.GetFiles(Path.Combine("core of the earth", "config")) == new string[1] { "baz.yml" }
                    && fs.GetParentDirectory("this") == "underground"
                    && fs.GetParentDirectory("underground") == "core of the earth"
                    && fs.GetParentDirectory("core of the earth") == (string)null);


            var dbcl = new DirectoryBasedConfigLookup(fileSystem);

            var cgs = dbcl.GetConfigGroups("this");

            var content = new StringBuilder();

            foreach (var cg in cgs)
            {
                content.Append(cg.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Baz: BazValue\r\nBar: BarValue\r\nFoo: FooValue\r\n"));
        }

        [Test]
        public void HierarchyWithSomeFilesTest()
        {
            var fileSystem =
                Mock.Of<IFilesSystem>(
                    fs =>
                    fs.Exists(Path.Combine("this", "config")) && fs.Exists(Path.Combine("underground", "config"))
                    && fs.Exists(Path.Combine("core of the earth", "config"))
                    && fs.GetFileContent("foo.yml") == "Foo: FooValue"
                    && fs.GetFileContent("bar.yml") == "Bar: BarValue"
                    && fs.GetFileContent("baz.yml") == "Baz: BazValue"
                    && fs.GetFileContent("foo2.yml") == "Foo2: Foo2Value"
                    && fs.GetFileContent("bar2.yml") == "Bar2: Bar2Value"
                    && fs.GetFileContent("baz2.yml") == "Baz2: Baz2Value"
                    && fs.GetFiles(Path.Combine("this", "config")) == new string[2] { "foo.yml", "foo2.yml" }
                    && fs.GetFiles(Path.Combine("underground", "config")) == new string[2] { "bar.yml", "bar2.yml" }
                    && fs.GetFiles(Path.Combine("core of the earth", "config")) == new string[2] { "baz.yml", "baz2.yml" }
                    && fs.GetParentDirectory("this") == "underground"
                    && fs.GetParentDirectory("underground") == "core of the earth"
                    && fs.GetParentDirectory("core of the earth") == (string)null);


            var dbcl = new DirectoryBasedConfigLookup(fileSystem);

            var cgs = dbcl.GetConfigGroups("this");

            var content = new StringBuilder();

            foreach (var cg in cgs)
            {
                content.Append(cg.RetrieveContent());
            }

            Assert.That(content.ToString(), Is.EqualTo("Baz: BazValue\r\nBaz2: Baz2Value\r\nBar: BarValue\r\nBar2: Bar2Value\r\nFoo: FooValue\r\nFoo2: Foo2Value\r\n"));
        }
    }
}