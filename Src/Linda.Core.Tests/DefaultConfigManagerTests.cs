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
        public void Test_SingleConfigSource_ShoulPassContentToDeserializer()
        {
            _configLookupStub.Setup(cl => cl.GetConfigGroups("this")).Returns(() =>
            {
                var configGroups = new List<ConfigGroup>{};

                var newConfigGroup = new ConfigGroup();

                newConfigGroup.AddConfigSource(new ConfigSource("Foo: FooValue"));

                configGroups.Add(newConfigGroup);

                return (IEnumerable<ConfigGroup>)configGroups;
            });

            var configLookup = _configLookupStub.Object;

            var deserelizer = new Mock<IYamlDeserializer>();

            deserelizer.Setup(d => d.Deserialize<SimpleConfig>(It.IsAny<string>()))
                       .Callback(
                           new Action<string>(
                               content =>
                                   {
                                       Assert.That(content, Is.Not.Null);
                                       Assert.That(content.Trim(), Is.EqualTo("Foo: FooValue"));
                                   }));

            var manager = new DefaultConfigManager(configLookup, deserelizer.Object, new ManualRootDetector("this"));


            //var result = 
                manager.GetConfig<SimpleConfig>();

//            var config = new SimpleConfig { Foo = "FooValue" };
//
//            Assert.That(result, Is.EqualTo(config));
        }

        [Test]
        public void OneCallGetConfigGroupsTest()
        {
            this._configLookupStub.Setup(cl => cl.GetConfigGroups("this")).Returns(() =>
            {
                var configGroups = new List<ConfigGroup>();

                var newConfigGroup = new ConfigGroup();

                newConfigGroup.AddConfigSource(new ConfigSource("Foo: FooValue"));

                configGroups.Add(newConfigGroup);

                return (IEnumerable<ConfigGroup>)configGroups;
            });

            var configLookup = this._configLookupStub.Object;

            var dcm = new DefaultConfigManager(configLookup, new CustomDeserializer(), new ManualRootDetector("this"));

            for (int i = 0; i < 1000; i++)
            {
                dcm.GetConfig<SimpleConfig>();
            }

            this._configLookupStub.Verify(cl => cl.GetConfigGroups("this"), Times.Once());
        }

        [Test]
        public void EmptyConfigGroupsTest()
        {
            this._configLookupStub.Setup(cl => cl.GetConfigGroups("this")).Returns(() =>
            {
                var configGroups = new List<ConfigGroup>();

                return (IEnumerable<ConfigGroup>)configGroups;
            });

            var configLookup = this._configLookupStub.Object;

            var dcm = new DefaultConfigManager(configLookup, new CustomDeserializer(), new ManualRootDetector("this"));

            var result = dcm.GetConfig<SimpleConfig>();

            var simpleConfig = new SimpleConfig();

            Assert.That(result, Is.EqualTo(simpleConfig));
        }
    }
}
