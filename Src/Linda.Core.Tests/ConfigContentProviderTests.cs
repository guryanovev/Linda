namespace Linda.Core.Tests
{
    using System;
    using System.Collections.Generic;

    using Linda.Core.AcceptanceTests;

    using NUnit.Framework;

    class ConfigContentProviderTests : TestsBase
    {
        [Test]
        public void GetConfigSourceContentTest()
        {
            this.CreateFile("foo.yml", "bar");

            var csc = new ConfigContentProvider();

            var content = csc.GetConfigSourceContent(new ConfigSource(this.GetFullPath("foo.yml")));

            Assert.That(content, Is.EqualTo("bar"));
        }

        [Test]
        public void GetConfigGroupContentTest()
        {
            this.CreateFile("foo.yml", "foo");
            this.CreateFile("bar.yml", "bar");

            var csc = new ConfigContentProvider();

            var configGroup = new ConfigGroup();

            configGroup.AddConfigSource(new ConfigSource(this.GetFullPath("foo.yml")));
            configGroup.AddConfigSource(new ConfigSource(this.GetFullPath("bar.yml")));

            var content = csc.GetConfigGroupContent(configGroup);

            Assert.That(content, Is.EqualTo("foo" + Environment.NewLine + "bar" + Environment.NewLine));
        }

        [Test]
        public void GetAllConfigContentTest()
        {
            this.CreateFile("foo.yml", "foo");
            this.CreateFile("config/bar.yml", "bar");

            var csc = new ConfigContentProvider();

            var configGroup1 = new ConfigGroup();
            var configGroup2 = new ConfigGroup();

            configGroup1.AddConfigSource(new ConfigSource(this.GetFullPath("foo.yml")));
            configGroup2.AddConfigSource(new ConfigSource(this.GetFullPath("config/bar.yml")));

            var content = csc.GetAllConfigContent(new List<ConfigGroup> {configGroup1, configGroup2});

            Assert.That(content, Is.EqualTo("foo" + Environment.NewLine + "bar" + Environment.NewLine));
        }
    }
}
