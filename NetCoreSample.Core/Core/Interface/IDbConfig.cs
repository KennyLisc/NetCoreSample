namespace NetCoreSample.Core.Core.Interface
{
    public interface IDbConfig
    {
        // string GetConnectionString();

        string ConnectionString { get; }
    }
}
