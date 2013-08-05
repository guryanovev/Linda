namespace Linda.Core.Tests
{
    using Linda.Core.AcceptanceTests.Support;
    using Linda.Core.Yaml;

    using NUnit.Framework;

    public class DeserializerTests
    {
        [Test]
        public void Test_TwoArgumentsInYamlString_ShouldReturnObjectWithTwoArguments()
        {
            const string YamlSimpleString = @"Foo: FooValue
Bar: BarValue";

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(YamlSimpleString);

            var config = new SimpleConfig() { Bar = "BarValue", Foo = "FooValue" };

            Assert.That(newSimpleConfig, Is.EqualTo(config));
        }

        [Test]
        public void Test_ThreeArgumentsInYamlString_ShouldReturnObjectWithTwoArguments()
        {
            const string YamlSimpleString = @"Foo: FooValue
Bar: BarValue
Baz: BazValue";

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(YamlSimpleString);

            var config = new SimpleConfig() { Bar = "BarValue", Foo = "FooValue" };

            Assert.That(newSimpleConfig, Is.EqualTo(config));
        }

        [Test]
        public void Test_SingleArgumentInYamlString_ShouldReturnObjectWithSingleNullProperty()
        {
            const string YamlSimpleString = @"Foo: FooValue";

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(YamlSimpleString);

            var config = new SimpleConfig { Bar = null, Foo = "FooValue" };

            Assert.That(newSimpleConfig, Is.EqualTo(config));
        }

        [Test]
        public void Test_OneArgumentInYamlString_ShouldReturnObjectWithTwoNullsProperties()
        {
            const string YamlSimpleString = @"Baz: BazValue";

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(YamlSimpleString);

            var simpleConfig = new SimpleConfig { Bar = null, Foo = null };

            Assert.That(newSimpleConfig, Is.EqualTo(simpleConfig));
        }

        [Test]
        public void Test_EmptyYamlString_ShouldReturnObjectWithTwoNullsProperties()
        {
            const string YamlSimpleString = "";

            var newSimpleConfig = new CustomDeserializer().Deserialize<SimpleConfig>(YamlSimpleString);

            var simpleConfig = new SimpleConfig();

            Assert.That(newSimpleConfig, Is.EqualTo(simpleConfig));
        }
    }
}
