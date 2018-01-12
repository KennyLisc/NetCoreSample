using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DummyDB
{
    public class AppState
    {
        public static string DefaultConnection { get; set; }

        public static void BuildSetting()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            AppState.DefaultConnection = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
