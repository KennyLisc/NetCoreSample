using NetCoreSample.Core.Core.Interface;
using System;

namespace ConsoleApp.Config
{
    public class DbConfig: IDbConfig
    {
        //private readonly Func<string> _getConnectionString;

        public DbConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

/*        public DbConfig(Func<string> getConnectionString)
        {
            //this._getConnectionString = getConnectionString;
            Console.WriteLine($"DbConfig constuction, {DateTime.Now:yyyyMMdd HH:mm:ss}");
        }

        public string GetConnectionString()
        {
            return _getConnectionString();
        }*/
    }
}
