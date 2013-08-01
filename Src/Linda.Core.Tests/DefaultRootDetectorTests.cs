namespace Linda.Core.Tests
{
    using Linda.Core.Detecting;

    using NUnit.Framework;

    class DefaultRootDetectorTests
    {
        [Test]
        public void Test_ShouldReturnRoot()
        {
            var detector = new DefaultRootDetector();

            detector.GetConfigRoot();
        }
    }
}
