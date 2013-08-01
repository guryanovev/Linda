namespace Linda.Core.AcceptanceTests
{
    using Linda.Core.AcceptanceTests.Support;
    using NUnit.Framework;

    public class FormatTests : TestsBase
    {
        [Test]
        public void Test_SimpleConfig_ShouldLoadConfiguration()
        {
            CreateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            var config = LoadConfig<SimpleConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Foo, Is.EqualTo("fooValue"));
            Assert.That(config.Bar, Is.EqualTo("barValue"));
        }

        [Test]
        public void Test_GrapthConfig_ShouldLoadConfiguration()
        {
            CreateFile(
                "config/config1.yml",
@"ChildConfig:
    Foo: fooValue
    Bar: barValue");

            var config = LoadConfig<ParentConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.ChildConfig, Is.Not.Null);
            Assert.That(config.ChildConfig.Foo, Is.EqualTo("fooValue"));
            Assert.That(config.ChildConfig.Bar, Is.EqualTo("barValue"));
        }

        [Test]
        public void Test_ShouldLoadSimpleConfig()
        {
            CreateFile("config/config1.txt", "Foo: fooValue");
            CreateFile("config/config2.yml", "Bar: barValue");

            var config = LoadConfig<SimpleConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Bar, Is.EqualTo("barValue"));
            Assert.That(config.Foo, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(YamlDotNet.Core.SyntaxErrorException))]
        public void Test_ExpectedExceptionTestWithWronfYamlFiles()
        {
            CreateFile("config/config1.yml", "Foo:fooValue");
            CreateFile("config/config2.yml", "Baz: bazValue");

            LoadConfig<SimpleConfig>();
        }
    }
}