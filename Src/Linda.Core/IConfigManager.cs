namespace Linda.Core
{
    using System;

    public interface IConfigManager
    {
        TConfig GetConfig<TConfig>() where TConfig : new();

        object GetConfig(Type type);

        /// <summary>
        /// Watch file system and invokes the callback on every change.
        /// </summary>
        /// <typeparam name="TConfig">config type</typeparam>
        /// <param name="callback">config callback</param>
        void WatchForConfig<TConfig>(Action<TConfig> callback) where TConfig : new();
    }
}