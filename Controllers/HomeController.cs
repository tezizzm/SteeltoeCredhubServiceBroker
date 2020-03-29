using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CredhubServiceBrokerSteeltoe.Models;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Microsoft.Extensions.Configuration;

namespace CredhubServiceBrokerSteeltoe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private CloudFoundryServicesOptions CloudFoundryServices { get; set; }

        public CredHubServiceOption _credHubServiceOption1;
        public CredHubServiceOption _credHubServiceOption2;
        private IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, 
            IOptionsSnapshot<CredHubServiceOption> credHubServiceOption,
            IOptions<CloudFoundryServicesOptions> servicesOptions,
            IConfiguration config)
        {
            _logger = logger;
            _credHubServiceOption1 = credHubServiceOption.Get("showme-creds");
            _credHubServiceOption2 = credHubServiceOption.Get("showme-creds2");
            CloudFoundryServices = servicesOptions.Value;
            _config = config;
        }

        public IActionResult Index()
        {
            foreach (var service in CloudFoundryServices.ServicesList)
            {
                Console.WriteLine($"{service.Name}");
                Console.WriteLine($"{service.Label}");
                foreach (var credential in service.Credentials.Values)
                {
                    Console.WriteLine($"{credential.Value}");
                }
            }

            foreach (var item in _config.AsEnumerable())
            {
                System.Console.WriteLine($"key: {item.Key}, value: {item.Value}");
            }

            var credhubService1Username = $"****************Credhub Service 1 Username: {_credHubServiceOption1.Credentials.Username} ****************";
            var credhubService1Password = $"****************Credhub Service 1 Password: {_credHubServiceOption1.Credentials.Password} ****************"; 
            var credhubService2Username = $"****************Credhub Service 2 Username: {_credHubServiceOption2.Credentials.Username} ****************";
            var credhubService2Password = $"****************Credhub Service 2 Password: {_credHubServiceOption2.Credentials.Password} ****************";

            System.Console.WriteLine(credhubService1Username);
            System.Console.WriteLine(credhubService1Password);

            System.Console.WriteLine(credhubService2Username);
            System.Console.WriteLine(credhubService2Password);

            ViewBag.Username1 = credhubService1Username;
            ViewBag.Password1 = credhubService1Password;

            ViewBag.Username2 = credhubService2Username;
            ViewBag.Password2 = credhubService2Password;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
