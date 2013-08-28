//namespace Linda.Core.Tests
//{
//    using System;
//    using System.IO;
//    using System.Linq;
//    using Linda.Core.Lookup;
//    using Moq;
//    using NUnit.Framework;
//
//    public class DirectoryBasedConfigLookupTests
//    {
//        [Test]
//        public void Test_SingleFile_ShouldReturnGroupWithSingleSource()
//        {
//            var fileSystem =
//                Mock.Of<IFilesSystem>(
//                    fs =>
//                    fs.Exists(Path.Combine("this", "config")) && fs.GetFileContent("foo.yml") == "Foo: FooValue"
//                    && fs.GetFiles(Path.Combine("this", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[1] { "foo.yml" }
//                    && fs.GetParentDirectory("this") == (string)null);
//
//            var lookup = new DirectoryBasedConfigLookup(fileSystem);
//
//            lookup.LoadConfigGroups("this");
//
//            var sources = lookup.ConfigGroups.ToArray();
//
//            Assert.That(sources.Length, Is.EqualTo(1));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo("Foo: FooValue"));
//        }
//
//        [Test]
//        public void Test_MultipleFiles_ShouldReturnGroupWithThreeSources()
//        {
//            var fileSystem =
//                Mock.Of<IFilesSystem>(
//                    fs =>
//                    fs.Exists(Path.Combine("this", "config")) && fs.GetFileContent("foo.yml") == "Foo: FooValue"
//                    && fs.GetFileContent("bar.yml") == "Bar: BarValue"
//                    && fs.GetFileContent("baz.yml") == "Baz: BazValue"
//                    && fs.GetFiles(Path.Combine("this", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[3] { "foo.yml", "bar.yml", "baz.yml" }
//                    && fs.GetParentDirectory("this") == (string)null);
//
//            var lookup = new DirectoryBasedConfigLookup(fileSystem);
//
//            var groups = lookup.LoadConfigGroups("this");
//
//            Assert.That(groups, Is.Not.Null);
//
//            var sources = groups.ToArray();
//            Assert.That(sources.Length, Is.EqualTo(1));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo("Foo: FooValue" + Environment.NewLine + "Bar: BarValue" + Environment.NewLine + "Baz: BazValue"));
//        }
//
//        [Test]
//        public void Test_HierarchyFiles_ShouldReturnGroupWithThreeSources()
//        {
//            var fileSystem =
//                Mock.Of<IFilesSystem>(
//                    fs =>
//                    fs.Exists(Path.Combine("this", "config"))
//                    && fs.Exists(Path.Combine("underground", "config"))
//                    && fs.Exists(Path.Combine("core of the earth", "config"))
//                    && fs.GetFileContent("foo.yml") == "Foo: FooValue" && fs.GetFileContent("bar.yml") == "Bar: BarValue"
//                    && fs.GetFileContent("baz.yml") == "Baz: BazValue"
//                    && fs.GetFiles(Path.Combine("this", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[1] { "foo.yml" }
//                    && fs.GetFiles(Path.Combine("underground", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[1] { "bar.yml" }
//                    && fs.GetFiles(Path.Combine("core of the earth", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[1] { "baz.yml" }
//                    && fs.GetParentDirectory("this") == "underground"
//                    && fs.GetParentDirectory("underground") == "core of the earth"
//                    && fs.GetParentDirectory("core of the earth") == (string)null);
//
//
//            var lookup = new DirectoryBasedConfigLookup(fileSystem);
//
//            var groups = lookup.LoadConfigGroups("this");
//
//            Assert.That(groups, Is.Not.Null);
//
//            var sources = groups.ToArray();
//            Assert.That(sources.Length, Is.EqualTo(3));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo("Baz: BazValue"));
//            Assert.That(sources[1].RetrieveContent().Trim(), Is.EqualTo("Bar: BarValue"));
//            Assert.That(sources[2].RetrieveContent().Trim(), Is.EqualTo("Foo: FooValue"));
//        }
//
//        [Test]
//        public void Test_HierarchyFiles_ShouldReturnGroupWithSixSources()
//        {
//            var fileSystem =
//                Mock.Of<IFilesSystem>(
//                    fs =>
//                    fs.Exists(Path.Combine("this", "config")) && fs.Exists(Path.Combine("underground", "config"))
//                    && fs.Exists(Path.Combine("core of the earth", "config"))
//                    && fs.GetFileContent("foo.yml") == "Foo: FooValue"
//                    && fs.GetFileContent("bar.yml") == "Bar: BarValue"
//                    && fs.GetFileContent("baz.yml") == "Baz: BazValue"
//                    && fs.GetFileContent("foo2.yml") == "Foo2: Foo2Value"
//                    && fs.GetFileContent("bar2.yml") == "Bar2: Bar2Value"
//                    && fs.GetFileContent("baz2.yml") == "Baz2: Baz2Value"
//                    && fs.GetFiles(Path.Combine("this", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[2] { "foo.yml", "foo2.yml" }
//                    && fs.GetFiles(Path.Combine("underground", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[2] { "bar.yml", "bar2.yml" }
//                    && fs.GetFiles(Path.Combine("core of the earth", "config"), "[a-zA-Z0-9\\._-]*.yml") == new string[2] { "baz.yml", "baz2.yml" }
//                    && fs.GetParentDirectory("this") == "underground"
//                    && fs.GetParentDirectory("underground") == "core of the earth"
//                    && fs.GetParentDirectory("core of the earth") == (string)null);
//
//
//            var lookup = new DirectoryBasedConfigLookup(fileSystem);
//
//            var groups = lookup.LoadConfigGroups("this");
//
//            Assert.That(groups, Is.Not.Null);
//
//            var sources = groups.ToArray();
//            Assert.That(sources.Length, Is.EqualTo(3));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo("Baz: BazValue" + Environment.NewLine + "Baz2: Baz2Value"));
//            Assert.That(sources[1].RetrieveContent().Trim(), Is.EqualTo("Bar: BarValue" + Environment.NewLine + "Bar2: Bar2Value"));
//            Assert.That(sources[2].RetrieveContent().Trim(), Is.EqualTo("Foo: FooValue" + Environment.NewLine + "Foo2: Foo2Value"));
//        }
//    }
//}