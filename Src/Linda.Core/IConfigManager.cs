namespace Linda.Core
{
    public interface IConfigManager
    {
        TConfig GetConfig<TConfig>();
    }
}