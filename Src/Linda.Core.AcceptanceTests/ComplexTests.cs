﻿namespace Linda.Core.AcceptanceTests
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
        public void TestLoadComplexConfig()
        {
            CreateFile("config/config1.yml", string.Empty);
            CreateFile("config/config2.yml", 
@"Foo: fooValue
Baz: bazValue
Bar: barValue
AnotherDict:
    one: 1
    two: 2
    three: 3
AnotherDate: 2002-12-04");

            var config = LoadConfig<ComplexConfig>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Bar, Is.EqualTo("barValue"));
            Assert.That(config.Date, Is.EqualTo(new DateTime()));
            Assert.That(config.DictProperty, Is.EqualTo(new Dictionary<string, ComplexConfig.Config>()));
            Assert.That(config.StructProperty, Is.EqualTo(new List<ComplexConfig.Struct>()));
        }

        [Test]
        public void TestLoadComplexConfigWithStruct()
        {
            CreateFile("config/config1.yml",
@"ListObjects:
    - objectClass:
        Str: str1
        Dict:
            one: 1
            two: 2
            three: 3
    - objectClass:
        Str: str2
        Dict:
            four: 4
            five: 5
Bar: barValue
StructProperty:
    - 
        Foo: fooValue1
        List:
            - 2.03
            - 1.007
            - 3.658
    - 
        Foo: fooValue2
        List:
            - 321.25
            - 98.25");

            var config = this.LoadConfig<ComplexConfig>();
            var config1 = new ComplexConfig();
            config1.Bar = "barValue";
            config1.Date = new DateTime();

            var structPropertyList = new List<ComplexConfig.Struct>();

            var list1 = new List<double> { 2.03, 1.007, 3.658 };

            var list2 = new List<double> { 321.25, 98.25 };

            structPropertyList.Add(new ComplexConfig.Struct { Foo = "fooValue1", List = new List<double>(list1) });
            structPropertyList.Add(new ComplexConfig.Struct { Foo = "fooValue2", List = new List<double>(list2) });

            config1.StructProperty = structPropertyList;

            Assert.That(config1.Equals(config), Is.True);
        }

        [Test]
        public void TestLoadComplexConfigWithDict()
        {
            CreateFile("config/config1.yml",
@"Bar: barValue
DictProperty:
    first:
            NumberOfConfig: 1
            IsProperty: true
            DoubleProperty: 45.277
            Bytes:
                - 123
                - 24
                - 13
                - 98
    second:
            NumberOfConfig: 2
            IsProperty: false
            DoubleProperty: 32.645
            Bytes:
                - 178
                - 36
                - 47
Date: 2002-08-24
List:
    - 1
    - 2
    - 3");

            var config = this.LoadConfig<ComplexConfig>();
            var config1 = new ComplexConfig();
            config1.Bar = "barValue";
            config1.Date = new DateTime(2002, 8, 24);
 
            var dict = new Dictionary<string, ComplexConfig.Config>();

            dict.Add("first", new ComplexConfig.Config { Bytes = new List<byte> { 123, 24, 13, 98 }, DoubleProperty = 45.277, IsProperty = true, NumberOfConfig = 1 });
            dict.Add("second", new ComplexConfig.Config { Bytes = new List<byte> { 178, 36, 47 }, DoubleProperty = 32.645, IsProperty = false, NumberOfConfig = 2 });

            config1.DictProperty = dict;
            /*Assert.That(config.Bar, Is.EqualTo("barValue"));
            Assert.That(config.Date, Is.EqualTo(new DateTime(2002, 8, 24)));
            Assert.That(config.StructProperty, Is.EqualTo(new List<ComplexConfig.Struct>()));
            Assert.That(config.DictProperty, Is.EquivalentTo(dict));*/

            Assert.That(config.Equals(config1), Is.True);
        }
    }
}