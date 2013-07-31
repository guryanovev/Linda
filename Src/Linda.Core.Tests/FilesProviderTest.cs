namespace Linda.Core.Tests
{
    using Linda.Core.AcceptanceTests;

    using System.IO;

    using Moq;

    using NUnit.Framework;

    class FilesProviderTest : TestsBase
    {
        [Test]
        public void SimpleTest()
        {
            this.CreateFile("foo.txt", string.Empty);

            var filesProviderStub = new Mock<IFilesSystem>();

            filesProviderStub.Setup(f => f.GetConfigGroupFromPath(It.IsAny<string>())).Returns<string>(
                path =>
                    {
                        var result = new ConfigGroup();

                        var filesString = new DirectoryInfo(this.GetFullPath(path)).GetFiles("*.txt");

                        foreach (var s in filesString)
                        {
                            result.AddConfigSource(new ConfigSource(s.FullName));
                        }

                        return result;
                    });

            var fp = filesProviderStub.Object;

            var configGroup = fp.GetConfigGroupFromPath(".");

            Assert.That(fp, Is.Not.Null);

            foreach (var c in configGroup)
            {
                Assert.That(c.Path, Is.EqualTo(this.GetFullPath() + "\\foo.txt"));
            }
        }


    }
}
