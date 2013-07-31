namespace Linda.Core.AcceptanceTests
{
    using System;

    using Linda.Core.AcceptanceTests.Support;

    using NUnit.Framework;

    public class AllTypeTests : TestsBase
    {
        [Test]
        [Culture("en-US")]
        public void Test_AllTypeConfig()
        {
            CreateFile(
                "config/config1.yml",
@"BoolProp: True
IntProp: 666
StringProp: Hello World
DateProp: 2013-07-27
DoubleProp: 1.333
FloatProp: 2.666");

            var config = LoadConfig<AllTypeConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.BoolProp, Is.EqualTo(true));
            Assert.That(config.IntProp, Is.EqualTo(666));
            Assert.That(config.FloatProp, Is.EqualTo((float)2.666));
            Assert.That(config.StringProp, Is.EqualTo("Hello World"));
            Assert.That(config.DoubleProp, Is.EqualTo(1.333));
            Assert.That(config.DateProp, Is.EqualTo(new DateTime(2013, 7, 27)));
        }

        [Test]
        public void Test_StructConfig()
        {
            CreateFile(
                "config/config1.yml",
@"ItIsStruct:
    Bar: 666
    Baz: BarValue");

            var config = LoadConfig<StructConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.ItIsStruct.Bar, Is.EqualTo(666));
            Assert.That(config.ItIsStruct.Baz, Is.EqualTo("BarValue"));
        }
    }
}
