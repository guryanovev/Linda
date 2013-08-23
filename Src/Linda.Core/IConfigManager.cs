namespace Linda.Core
{
    using System;

    public interface IConfigManager
    {
        TConfig GetConfig<TConfig>() where TConfig : new();

        void WatchForConfig<TConfig>(Action<TConfig> callback);
    }
}