using System;
using System.Threading.Tasks;
using ConsoleApp.Core;
using ConsoleApp.Services;
using Microsoft.Extensions.Logging;
using NetCoreSample.Core.Core.Interface;
using NetCoreSample.Core.Domain;

namespace ConsoleApp
{
    public class App
    {
        private readonly ITestService _testService;
        private readonly ILogger<App> _logger;
        // private readonly AppSettings _config;
        private readonly IRaceService _raceService;
        private readonly IUserScoreService _userScoreService;
        private readonly IUserScoreDBSearch _userScoreDBSearch;
        private readonly IRaceDBSearch _raceDBSearch;

        public App(ITestService testService, ILogger<App> logger, IRaceService raceService, IUserScoreService userScoreService, IUserScoreDBSearch userScoreDBSearch, IRaceDBSearch raceDBSearch)
        {
            _testService = testService;
            _logger = logger;
            _raceService = raceService;
            _userScoreService = userScoreService;
            _userScoreDBSearch = userScoreDBSearch;
            _raceDBSearch = raceDBSearch;
            Console.WriteLine("App constructor");
        }

        public async Task Run()
        {
            // return;
            //_logger.LogInformation($"This is a console application for {_config.Title}");
            _testService.Run();

            try
            {
                var id = new Random().Next(1, 1000);
                var newUserScoreItem = new UserScore
                {
                    Id = Guid.NewGuid(),
                    UserName = $"Lsc_{id}",
                    Score = id
                };
                await _userScoreService.Insert(newUserScoreItem);

                var newItem = new Race
                {
                    //Id = Guid.NewGuid(),
                    Name = "Race 1",
                    Date = DateTime.Now,
                    Location = "Suzhou"
                };

                Console.WriteLine(newItem.Date);

                Console.WriteLine(newItem.Id);
                await _raceService.Insert(newItem);

                Console.WriteLine(newItem.Id);

                var items = await _raceService.Search("Race");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id}, {item.Name}, {item.Date:yyyy-MM-dd HH:mm}");
                }

                var scoreItems = await _userScoreService.Search("L");
                foreach (var item in scoreItems)
                {
                    Console.WriteLine($"{item.Id}, {item.UserName}, {item.Score}");
                }

                Console.WriteLine("Search by Dapper....");

                var raceItems = await _raceDBSearch.SearchRaceList("a");
                foreach (var item in raceItems)
                {
                    Console.WriteLine($"{item.Id}, {item.Name}, {item.Date:yyyy-MM-dd HH:mm}");
                }

                var userScoreItems = await _userScoreDBSearch.SearchList("L");
                foreach (var item in userScoreItems)
                {
                    Console.WriteLine($"{item.Id}, {item.UserName}, {item.Score}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }

            System.Console.ReadKey();
        }
    }
}
