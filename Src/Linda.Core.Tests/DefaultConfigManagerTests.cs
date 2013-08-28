//namespace Linda.Core.Tests
//{
//    using System;
//    using System.Collections.Generic;
//
//    using Linda.Core.AcceptanceTests.Support;
//    using Linda.Core.Detecting;
//    using Linda.Core.Lookup;
//    using Linda.Core.Yaml;
//
//    using Moq;
//    using NUnit.Framework;
//
//    public class DefaultConfigManagerTests
//    {
//        private Mock<IConfigLookup> _configLookupStub;
//
//        [SetUp]
//        public void Init()
//        {
//            _configLookupStub = new Mock<IConfigLookup>();
//        }
//
//        [Test]
//        public void Test_SingleConfigSource_ShouldPassContentToDeserializer()
//        {
//            _configLookupStub.Setup(cl => cl.LoadConfigGroups("this")).Returns(() =>
//            {
//                var configGroups = new List<ConfigGroup>
//                                       {
//                                           new ConfigGroup(new ConfigSource("Foo: FooValue"))
//                                       };
//
//                return (IEnumerable<ConfigGroup>)configGroups;
//            });
//
//            var configLookup = _configLookupStub.Object;
//
//            var deserializer = new Mock<IYamlDeserializer>();
//
//            deserializer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
//                       .Callback(
//                           new Action<string>(
//                               content =>
//                               {
//                                   Assert.That(content, Is.Not.Null);
//                                   Assert.That(content.Trim(), Is.EqualTo("Foo: FooValue"));
//                               }));
//
//            var manager = new DefaultConfigManager(configLookup, deserializer.Object, new ManualRootDetector("this"));
//
//            manager.GetConfig<SimpleConfig>();
//        }
//
//        [Test]
//        public void Test_SomeoneCallsGetConfig_ShouldOnceCallGetConfigGroups()
//        {
//            _configLookupStub.Setup(cl => cl.LoadConfigGroups("this")).Returns(
//                () =>
//                    {
//                        var configGroups = new List<ConfigGroup>();
//
//                        return (IEnumerable<ConfigGroup>)configGroups;
//                    });
//
//            var configLookup = _configLookupStub.Object;
//
//            var dcm = new DefaultConfigManager(configLookup, new CustomDeserializer(), new ManualRootDetector("this"));
//
//            for (int i = 0; i < 1000; i++)
//            {
//                dcm.GetConfig<SimpleConfig>();
//            }
//
//            _configLookupStub.Verify(cl => cl.LoadConfigGroups("this"), Times.Once());
//        }
//
//        [Test]
//        public void Test_TwoConfigSource_ShouldPassMergeContentToDeserializer()
//        {
//            _configLookupStub.Setup(cl => cl.LoadConfigGroups("this")).Returns(() =>
//            {
//                var configGroups = new List<ConfigGroup>
//                                       {
//                                           new ConfigGroup(new ConfigSource("Foo: FooValue"), new ConfigSource("Bar: BarValue"))
//                                       };
//
//                return (IEnumerable<ConfigGroup>)configGroups;
//            });
//
//            var configLookup = _configLookupStub.Object;
//
//            var deserializer = new Mock<IYamlDeserializer>();
//
//            deserializer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
//                       .Callback(
//                           new Action<string>(
//                               content =>
//                               {
//                                   Assert.That(content, Is.Not.Null);
//                                   Assert.That(content.Trim(), Is.EqualTo("Foo: FooValue" + Environment.NewLine + "Bar: BarValue"));
//                               }));
//
//            var manager = new DefaultConfigManager(configLookup, deserializer.Object, new ManualRootDetector("this"));
//
//            manager.GetConfig<SimpleConfig>();
//        }
//
//        [Test]
//        public void Test_MultipleConfigGroup_ShouldPassMergeContentToDeserializer()
//        {
//            _configLookupStub.Setup(cl => cl.LoadConfigGroups("this")).Returns(() =>
//            {
//                var configGroups = new List<ConfigGroup>
//                                       {
//                                           new ConfigGroup(new ConfigSource("Foo: FooValue")),
//                                           new ConfigGroup(new ConfigSource("Bar: BarValue"))
//                                       };
//
//                return (IEnumerable<ConfigGroup>)configGroups;
//            });
//
//            var configLookup = _configLookupStub.Object;
//
//            var deserializer = new Mock<IYamlDeserializer>();
//
//            deserializer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
//                       .Callback(
//                           new Action<string>(
//                               content =>
//                               {
//                                   Assert.That(content, Is.Not.Null);
//                                   Assert.That(content.Trim(), Is.EqualTo("Foo: FooValue" + Environment.NewLine + "Bar: BarValue"));
//                               }));
//
//            var manager = new DefaultConfigManager(configLookup, deserializer.Object, new ManualRootDetector("this"));
//
//            manager.GetConfig<SimpleConfig>();
//        }
//    }
//}
