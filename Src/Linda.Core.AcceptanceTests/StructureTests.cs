namespace Linda.Core.AcceptanceTests
{
    using Linda.Core.AcceptanceTests.Support;
    using NUnit.Framework;

    public class StructureTests : TestsBase
    {
        [Test]
        public void Tests_MultipleFiles_ShouldMergeValues()
        {
            CreateFile("config/config1.yaml", "Bar: barValue");
            CreateFile("config/config2.yaml", "Foo: fooValue");

            var config = LoadConfig<SimpleConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Foo, Is.EqualTo("fooValue"));
            Assert.That(config.Bar, Is.EqualTo("barValue"));
        }
    }
}