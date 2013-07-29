using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linda.Core.AcceptanceTests.Support;
using NUnit.Framework;
namespace Linda.Core.AcceptanceTests
{
    class AllTypeTests : TestsBase
    {
        [Test]
        [Culture("de-DE")]
        public void Test_AllTypeConfig()
        {
            CreateFile(
                "config/config1.yml",
@"ByteArrayProp:
    - 0
    - 1
    - 2");

            var config = LoadConfig<AllTypeConfig>();

            
            //Assert.That(config.DateProp, Is.EqualTo(new DateTime(2013, 7, 27)));
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
