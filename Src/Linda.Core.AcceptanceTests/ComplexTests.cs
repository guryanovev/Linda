namespace Linda.Core.AcceptanceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Linda.Core.AcceptanceTests.Support;
    using NUnit.Framework;

    public class ComplexTests : TestsBase
    {
        [Test]
        public void ComplexTestLoadNullConfig()
        {
            CreateFile("config/config1.yml", string.Empty);

            var config = LoadConfig<ComplexConfig>();

            Assert.That(config, Is.Null);
        }

        [Test]
        public void ComplexTestLoadComplexConfig()
        {
            CreateFile("config/config1.yml", string.Empty);
            CreateFile("config/config2.yml", 
@"Foo: fooValue
Baz: bazValue
Bar: barValue
AnotherDict:
    one: 1
    two: 2
    three: 3");

            var config = LoadConfig<ComplexConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Bar, Is.EqualTo("barValue"));
            Assert.That(config.Date, Is.EqualTo(new DateTime()));
            Assert.That(config.DictProperty, Is.EqualTo(new Dictionary<string, ComplexConfig.Config>()));
            Assert.That(config.StructProperty, Is.EqualTo(new List<ComplexConfig.StructProp>()));
        }
    }
}
