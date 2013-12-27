namespace Linda.Core.AcceptanceTests
{
    using System;
    using System.Threading;

    using Linda.Core.AcceptanceTests.Support;
    using Linda.Core.Lookup;
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

            using (var configManager = CreateConfigManager())
            {
                var callback = new TestableAction<SimpleConfig>();
                configManager.WatchForConfig<SimpleConfig>(callback);
                Thread.Sleep(500);
                callback.AssertCalled(1);
            }
        }

        [Test]
        public void Test_AddNonConfigToDirectoryBased_ShouldCallActionOnce()
        {
            CreateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            using (var configManager = CreateConfigManager())
            {
                var callback = new TestableAction<SimpleConfig>();
                configManager.WatchForConfig<SimpleConfig>(callback);

                CreateFile("config/not-config.txt", @"it is not a config file");
                UpdateFile("config/not-config.txt", @"it is not a config file 2");

                Thread.Sleep(500);
                callback.AssertCalled(1);
            }
        }

        [Test]
        public void Test_AddNonConfigToFileBased_ShouldCallActionOnce()
        {
            CreateFile(
                "fileConfig.yml",
@"Foo: fooValue
Bar: barValue");

            using (var configManager = CreateConfigManager(new FileBasedConfigLookup()))
            {
                var callback = new TestableAction<SimpleConfig>();
                configManager.WatchForConfig<SimpleConfig>(callback);

                CreateFile("not-config.txt", @"it is not a config file");
                UpdateFile("not-config.txt", @"it is not a config file 2");

                Thread.Sleep(500);
                callback.AssertCalled(1);
            }
        }

        [Test]
        public void Test_FileChanged_ShouldCallActionTwice()
        {
            CreateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            using (var configManager = CreateConfigManager(new DirectoryBasedConfigLookup()))
            {
                var callback = new TestableAction<SimpleConfig>();
                configManager.WatchForConfig<SimpleConfig>(callback);

                UpdateFile("config/config1.yml", @"Foo: fooValue
Bar: barValue");
                Thread.Sleep(500);
                callback.AssertCalled(3);
            }
        }

        /// <summary>
        /// A regression test for #1
        /// see https://github.com/guryanovev/Linda/issues/1
        /// </summary>
        [Test]
        public void Test_DeleteFileWhileNoWatching_ShouldNotFail()
        {
            CreateFile(
                "config/config1.yml",
@"Foo: fooValue
Bar: barValue");

            using (var configManager = CreateConfigManager(new DirectoryBasedConfigLookup()))
            {
                configManager.GetConfig<SimpleConfig>();

                DeleteFileOrDirectory("config");
                CreateFile(
                    "config/config1.yml",
@"Foo: fooValue
Bar: barValue");
            }

            // if we get here without an error then the test is complete!
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