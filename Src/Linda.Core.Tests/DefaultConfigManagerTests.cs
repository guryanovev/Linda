namespace Linda.Core.Tests
{
    using System;
    using System.Collections.Generic;

    using Linda.Core.AcceptanceTests.Support;
    using Linda.Core.Detecting;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    using Moq;
    using NUnit.Framework;

    public class DefaultConfigManagerTests
    {
        private Mock<IConfigLookup> _configLookupStub;

        [SetUp]
        public void Init()
        {
            _configLookupStub = new Mock<IConfigLookup>();
        }

        [Test]
        public void Test_SingleConfigSource_ShouldPassContentToDeserializer()
        {
            _configLookupStub.Setup(cl => cl.ConfigGroups)
                             .Returns(new List<ConfigGroup> { new ConfigGroup(new ConfigSource("Foo: fooValue")) });

            _configLookupStub.Setup(cl => cl.GetContent()).Returns("Foo: fooValue");

            var configLookup = _configLookupStub.Object;

            var deserializer = new Mock<IYamlDeserializer>();

            deserializer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
                       .Callback(
                           new Action<string>(
                               content =>
                               {
                                   Assert.That(content, Is.Not.Null);
                                   Assert.That(content.Trim(), Is.EqualTo("Foo: fooValue"));
                               }));

            var manager = new DefaultConfigManager(configLookup, deserializer.Object, new ManualRootDetector("this"));

            manager.GetConfig<SimpleConfig>();
        }

        [Test]
        public void Test_SomeoneCallsGetConfig_ShouldOnceCallGetConfigGroups()
        {
            _configLookupStub.Setup(cl => cl.ConfigGroups)
                 .Returns(new List<ConfigGroup> { new ConfigGroup(new ConfigSource("Foo: fooValue")) });

            _configLookupStub.Setup(cl => cl.GetContent()).Returns("Foo: fooValue");

            var configLookup = _configLookupStub.Object;

            var dcm = new DefaultConfigManager(configLookup, new CustomDeserializer(), new ManualRootDetector("this"));

            for (int i = 0; i < 1000; i++)
            {
                dcm.GetConfig<SimpleConfig>();
            }

            _configLookupStub.Verify(cl => cl.LoadConfigGroups("this"), Times.Once());
        }

        [Test]
        public void Test_TwoConfigSource_ShouldPassMergeContentToDeserializer()
        {
            _configLookupStub.Setup(cl => cl.ConfigGroups)
                 .Returns(new List<ConfigGroup> { new ConfigGroup(new ConfigSource("Foo: fooValue")) });

            _configLookupStub.Setup(cl => cl.GetContent()).Returns("Foo: fooValue" + Environment.NewLine + "Bar: barValue");

            var configLookup = _configLookupStub.Object;

            var deserializer = new Mock<IYamlDeserializer>();

            deserializer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
                       .Callback(
                           new Action<string>(
                               content =>
                               {
                                   Assert.That(content, Is.Not.Null);
                                   Assert.That(content.Trim(), Is.EqualTo("Foo: fooValue" + Environment.NewLine + "Bar: barValue"));
                               }));

            var manager = new DefaultConfigManager(configLookup, deserializer.Object, new ManualRootDetector("this"));

            manager.GetConfig<SimpleConfig>();
        }

        [Test]
        public void Test_MultipleConfigGroup_ShouldPassMergeContentToDeserializer()
        {
            _configLookupStub.Setup(cl => cl.ConfigGroups)
                             .Returns(new List<ConfigGroup> { new ConfigGroup(new ConfigSource("Foo: fooValue")) });

            _configLookupStub.Setup(cl => cl.GetContent()).Returns("Foo: fooValue" + Environment.NewLine + "Bar: barValue");

            var configLookup = _configLookupStub.Object;

            var deserializer = new Mock<IYamlDeserializer>();

            deserializer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
                       .Callback(
                           new Action<string>(
                               content =>
                               {
                                   Assert.That(content, Is.Not.Null);
                                   Assert.That(content.Trim(), Is.EqualTo("Foo: fooValue" + Environment.NewLine + "Bar: barValue"));
                               }));

            var manager = new DefaultConfigManager(configLookup, deserializer.Object, new ManualRootDetector("this"));

            manager.GetConfig<SimpleConfig>();
        }
    }
}
