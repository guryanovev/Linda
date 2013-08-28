//namespace Linda.Core.Tests
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using Linda.Core.Lookup;
//    using Moq;
//    using NUnit.Framework;
//
//    public class FileBasedConfigLookupTests
//    {
//        [Test]
//        public void Test_SingleFile_ShouldReturnGroupWithSingleSource()
//        {
//            var filesSystem =
//                Mock.Of<IFilesSystem>(
//                    f =>
//                    f.Exists("path") &&
//                    f.GetFiles("path", "[a-zA-Z0-9\\._-]*.yml") == new List<string> { "path/config.yml" }
//                    && f.GetFileContent("path/config.yml") == "Foo: fooValue"
//                    && f.GetParentDirectory("path") == (string)null);
//
//            var lookup = new FileBasedConfigLookup(filesSystem);
//            lookup.SearchPatternRegEx = "[a-zA-Z0-9\\._-]*.yml";
//
//            var groups = lookup.LoadConfigGroups("path").ToList();
//
//            Assert.That(groups, Is.Not.Null);
//
//            var sources = groups.ToArray();
//            Assert.That(sources.Length, Is.EqualTo(1));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo("Foo: fooValue"));
//        }
//
//        [Test]
//        public void Test_MultipleFiles_ShouldReturnGroupWithSingleSource()
//        {
//            var filesSystem =
//                Mock.Of<IFilesSystem>(
//                    f =>
//                    f.Exists("path")
//                    && f.GetFiles("path", "[a-zA-Z0-9\\._-]*.yml") == new List<string> { "path/foo.yml", "path/bar.yml" }
//                    && f.GetFileContent("path/foo.yml") == "Foo: fooValue"
//                    && f.GetFileContent("path/bar.yml") == "Bar: barValue"
//                    && f.GetParentDirectory("path") == (string)null);
//
//            var lookup = new FileBasedConfigLookup(filesSystem);
//            lookup.SearchPatternRegEx = "[a-zA-Z0-9\\._-]*.yml";
//
//            var groups = lookup.LoadConfigGroups("path").ToList();
//
//            Assert.That(groups, Is.Not.Null);
//
//            var sources = groups.ToArray();
//            Assert.That(sources.Length, Is.EqualTo(1));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo("Foo: fooValue" + Environment.NewLine + "Bar: barValue"));
//        }
//
//        [Test]
//        public void Test_HierarchyFiles_ShouldReturnGroupWithFourSource()
//        {
//            var filesSystem =
//                Mock.Of<IFilesSystem>(
//                    f =>
//                    f.Exists("path") && f.Exists("under path") && f.Exists("under path")
//                    && f.Exists("parent under path") && f.GetFiles("path", "[a-zA-Z0-9\\._-]*.yml") == new List<string> { "path/config1.yml" }
//                    && f.GetFiles("under path", "[a-zA-Z0-9\\._-]*.yml")
//                       == new List<string> { "under path/config2.yml", "under path/config3.yml" }
//                    && f.GetFiles("parent under path", "[a-zA-Z0-9\\._-]*.yml") == new List<string> { "parent under path/config4.yml" }
//                    && f.GetFileContent("path/config1.yml") == "Foo: fooValue"
//                    && f.GetFileContent("under path/config2.yml") == "Bar: barValue"
//                    && f.GetFileContent("under path/config3.yml") == "Baz: bazValue"
//                    && f.GetFileContent("parent under path/config4.yml") == "FooBar: fooBarValue"
//                    && f.GetParentDirectory("path") == "under path"
//                    && f.GetParentDirectory("under path") == "parent under path"
//                    && f.GetParentDirectory("parent under path") == (string)null);
//
//            var lookup = new FileBasedConfigLookup(filesSystem);
//            lookup.SearchPatternRegEx = "[a-zA-Z0-9\\._-]*.yml";
//
//            var groups = lookup.LoadConfigGroups("path").ToList();
//
//            Assert.That(groups, Is.Not.Null);
//
//            var sources = groups.ToArray();
//            Assert.That(sources.Length, Is.EqualTo(3));
//            Assert.That(sources[0].RetrieveContent().Trim(), Is.EqualTo(@"FooBar: fooBarValue"));
//            Assert.That(sources[1].RetrieveContent().Trim(), Is.EqualTo("Bar: barValue" + Environment.NewLine + "Baz: bazValue"));
//            Assert.That(sources[2].RetrieveContent().Trim(), Is.EqualTo(@"Foo: fooValue"));
//        }
//    }
//}
