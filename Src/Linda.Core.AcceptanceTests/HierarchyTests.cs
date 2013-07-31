namespace Linda.Core.AcceptanceTests
{
    using Linda.Core.AcceptanceTests.Support;
    using NUnit.Framework;

    public class HierarchyTests : TestsBase
    {
        [Test]
        public void Test_SameValueOnMultipleValues_ShouldUseBottomLevelValue()
        {
            CreateFile("config/config.yml", "Foo: topLevelValue");
            CreateFile("app/config/config.yml", "Foo: appLevelValue");

            var config = LoadConfig<SimpleConfig>("app");

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Foo, Is.EqualTo("appLevelValue"));
        }

        [Test]
        public void Test_DifferentValuesOnDifferentLevels_ShouldMergeValues()
        {
            CreateFile("config/config.yml", "Foo: fooValue");
            CreateFile("app/config/config.yml", "Bar: barValue");

            var config = LoadConfig<SimpleConfig>("app");

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Foo, Is.EqualTo("fooValue"));
            Assert.That(config.Bar, Is.EqualTo("barValue"));
        }

        [Test]
        public void HierarchyTestWithAnotherFiles()
        {
            CreateFile("config/config.yml", "Foo: fooValue");
            CreateFile("app/config/config.txt", "Bar: barValue");

            var config = LoadConfig<SimpleConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Foo, Is.EqualTo("fooValue"));
            Assert.That(config.Bar, Is.Null);
        }
    }
}