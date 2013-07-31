namespace Linda.Core.Tests
{
    using System;
    using System.Collections.Generic;

    using Linda.Core.AcceptanceTests.Support;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    using Moq;
    using NUnit.Framework;

    public class DefaultConfigManagerTests
    {
        [Test]
        public void SimpleTest()
        {
            var configLookupStub = new Mock<IConfigLookup>();

            configLookupStub.Setup(cl => cl.GetConfigGroups("this")).Returns(() =>
                {
                    var configGroups = new List<ConfigGroup>();

                    var newConfigGroup = new ConfigGroup();

                    newConfigGroup.AddConfigSource(new ConfigSource("Foo: FooValue"));

                    configGroups.Add(newConfigGroup);

                    return (IEnumerable<ConfigGroup>)configGroups;
                });

            var configLookup = configLookupStub.Object;

            var dcm = new DefaultConfigManager("this", configLookup, new CustomDeserializer());

            var result = dcm.GetConfig<SimpleConfig>();

            var sc = new SimpleConfig { Foo = "FooValue" };

            Assert.That(result, Is.EqualTo(sc));
        }

        [Test]
        public void OneCallGetConfigGroupsTest()
        {
            var configLookupStub = new Mock<IConfigLookup>();

            configLookupStub.Setup(cl => cl.GetConfigGroups("this")).Returns(() =>
            {
                var configGroups = new List<ConfigGroup>();

                var newConfigGroup = new ConfigGroup();

                newConfigGroup.AddConfigSource(new ConfigSource("Foo: FooValue"));

                configGroups.Add(newConfigGroup);

                return (IEnumerable<ConfigGroup>)configGroups;
            });

            var configLookup = configLookupStub.Object;

            var dcm = new DefaultConfigManager("this", configLookup, new CustomDeserializer());

            dcm.GetConfig<SimpleConfig>();

            dcm.GetConfig<SimpleConfig>();

            dcm.GetConfig<SimpleConfig>();

            configLookupStub.Verify(cl => cl.GetConfigGroups("this"), Times.Once());
        }

        [Test]
        public void EmptyConfigGroupsTest()
        {
            var configLookupStub = new Mock<IConfigLookup>();

            configLookupStub.Setup(cl => cl.GetConfigGroups("this")).Returns(() =>
            {
                var configGroups = new List<ConfigGroup>();

                return (IEnumerable<ConfigGroup>)configGroups;
            });

            var configLookup = configLookupStub.Object;

            var dcm = new DefaultConfigManager("this", configLookup, new CustomDeserializer());

            var result = dcm.GetConfig<SimpleConfig>();

            Assert.That(result, Is.Null);
        }
    }
}
