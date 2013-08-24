namespace Linda.Core.AcceptanceTests
{
    using System;
    using Linda.Core.AcceptanceTests.Support;
    using NUnit.Framework;

    public class WatchingTests : TestsBase
    {
        [Test]
        public void Test_NoChanges_ShouldCallActionOnce()
        {
            CreateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            var configManager = CreateConfigManager();
            var callback = new TestableAction<SimpleConfig>();
            configManager.WatchForConfig<SimpleConfig>(callback);

            callback.AssertCalled(1);
        }

        [Test]
        public void Test_FileChanged_ShouldCallActionTwice()
        {
            CreateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            var configManager = CreateConfigManager();
            var callback = new TestableAction<SimpleConfig>();
            configManager.WatchForConfig<SimpleConfig>(callback);

            UpdateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            callback.AssertCalled(2);
        }

        internal class TestableAction<TConfig>
        {
            private readonly Action<TConfig> _callback;
            private int _invokesCount;

            public TestableAction()
            {
                _callback = config => _invokesCount++;
            }

            public static implicit operator Action<TConfig>(TestableAction<TConfig> tester)
            {
                return tester._callback;
            }

            public void AssertCalled(int times)
            {
                Assert.That(
                    _invokesCount, 
                    Is.EqualTo(times),
                    "Action invoked times");
            }
        }
    }
}