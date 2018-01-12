using ConsoleApp.Core;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using FXTech.PDCA.Core.Interfaces.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreSample.Core.Domain;
using NetCoreSample.Core.Domain.User;
using NetCoreSample.Data;

namespace ConsoleApp
{
    // To product EXE use a command like this: dotnet publish -c Release -r win7-x64
    // See: https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli
    class Program
    {
        // public static IConfigurationRoot Configuration { get; set; }
        static async Task Main(string[] args)
        {
            // Adding JSON file into IConfiguration.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var log = serviceProvider.GetService<ILogger<Program>>();
            log.LogDebug("Test Log debug");
            log.LogInformation("Test Log");

            var smtpConfig = serviceProvider.GetService<IOptions<SmtpConfig>>().Value;
            // TestSendMail(smtpConfig);
            Console.WriteLine($"Your smtp setting is: {smtpConfig.Server}, {smtpConfig.User}, {smtpConfig.Port}");

            // run app
            await serviceProvider.GetService<App>().Run();
        }

        private static void TestSendMail(SmtpConfig config)
        {
            SmtpClient client = new SmtpClient(config.Server);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(config.User, config.Pass);
            client.Port = config.Port;
            client.EnableSsl = config.EnableSsl;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(config.From);
            mailMessage.To.Add("516101223@qq.com");
            mailMessage.Body = "测试邮件发送 内容!!!";
            mailMessage.Subject = "测试邮件发送";
            client.Send(mailMessage);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add configured instance of logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());
            serviceCollection.AddLogging();

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            serviceCollection.AddOptions();

            // add logging

            /*
            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", false)
                .Build();*/
            serviceCollection.Configure<SmtpConfig>(configuration.GetSection("Smtp"));


            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            // add services 
            serviceCollection.AddTransient<ITestService, TestService>();

            //add db
            
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddTransient<IRepository<Race>, BaseRepository<Race>>();
            serviceCollection.AddTransient<IRaceService, RaceService>();

            serviceCollection.AddTransient<IRepository<UserScore>, BaseRepository<UserScore>>();
            serviceCollection.AddTransient<IUserScoreService, UserScoreService>();

            // add app
            serviceCollection.AddTransient<App>();
        }
    }
}

/*
           // Read configuration
           string FirstName = config["FirstName"];
           string LastName = config["LastName"];
           Console.WriteLine($"Hello {FirstName} {LastName}!");

           var address = config.GetSection("Address");
           string street = address["Street"];
           string city = address["City"];
           string zip = address["Zip"];
           string state = address["State"];
           Console.WriteLine($"Your address is: {street}, {city}, {state} {zip}");

           var smtpConfig = new SmtpConfig();
           config.GetSection("Smtp").Bind(smtpConfig);

           Console.WriteLine($"Your smtp setting is: {smtpConfig.Server}, {smtpConfig.User}, {smtpConfig.Port}");
           */
