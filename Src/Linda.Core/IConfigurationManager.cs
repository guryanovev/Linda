namespace Linda.Core
{
    public interface IConfigurationManager
    {
        TConfiguration GetConfiguration<TConfiguration>();
    }
}