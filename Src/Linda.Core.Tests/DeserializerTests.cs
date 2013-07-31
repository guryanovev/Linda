namespace Linda.Core.Tests
{
    using System.IO;
    using System.Text;

    using Linda.Core.AcceptanceTests;
    using Linda.Core.AcceptanceTests.Support;
    using Linda.Core.Yaml;

    using NUnit.Framework;

    using YamlDotNet.RepresentationModel.Serialization;

    class DeserializerTests
    {
        [Test]
        public void DeserializeTest()
        {
            var simpleConfig = new SimpleConfig { Bar = "Bar", Foo = "Foo" };

            var yamlSimpleConfig = new StringBuilder();

            new Serializer().Serialize(new StringWriter(yamlSimpleConfig), simpleConfig);

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(new StringReader(yamlSimpleConfig.ToString()));

            Assert.That(newSimpleConfig, Is.EqualTo(simpleConfig));
        }

        [Test]
        public void DeserializeWrongAttributesTest()
        {
            var simpleConfig = new SimpleConfig { Bar = "Bar", Foo = "Foo" };

            var yamlSimpleConfig = new StringBuilder();

            new Serializer().Serialize(new StringWriter(yamlSimpleConfig), simpleConfig);

            yamlSimpleConfig.AppendLine("Baz: Baz");

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(new StringReader(yamlSimpleConfig.ToString()));

            Assert.That(newSimpleConfig, Is.EqualTo(simpleConfig));
        }

        [Test]
        public void DeserializeNotEnoughAttributesTest()
        {
            var simpleConfig = new SimpleConfig { Bar = null, Foo = "Foo" };

            var yamlSimpleConfig = new StringBuilder();

            yamlSimpleConfig.Append("Foo: Foo");

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(new StringReader(yamlSimpleConfig.ToString()));

            Assert.That(newSimpleConfig, Is.EqualTo(simpleConfig));
        }

        [Test]
        public void DeserializeAllNullAttributesTest()
        {
            var simpleConfig = new SimpleConfig { Bar = null, Foo = null };

            var yamlSimpleConfig = new StringBuilder().Append("Baz : baz");

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(new StringReader(yamlSimpleConfig.ToString()));

            Assert.That(newSimpleConfig, Is.EqualTo(simpleConfig));
        }

        [Test]
        public void DeserializeEmptyYamlStringTest()
        {
            var yamlSimpleConfig = new StringBuilder();

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(new StringReader(yamlSimpleConfig.ToString()));

            Assert.That(newSimpleConfig, Is.EqualTo(null));
        }

    }
}
