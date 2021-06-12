using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers//name space echivalent cu package uri
{
    [Authorize]// adnotari echivalente cu "@" din java;
               // acest controller nu va putea fi accesat doar dupa ce ne-am logat, altfel eroare: 403 forbidden
               //daca il stergi il poate ccesa oricine
    [ApiController] // se pune pete tot sa marcheze clasa ca fiind API Controller echivalent 
    [Route("api/[controller]")]//specifica ruta pentru care se vor apela metodele din controller
                           // [controller] ruta este data de WeatherForecast 
                           //ca sa apelez metode din weatherforecast apelez din postman->numele clasei fara controller
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]//array de stringuri read only si static
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;//poate afisa metode de logging(daca s-a facut request, userul care a facut request)

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]//metoda apeleaza canfd facem un get din postamn
        public IEnumerable<WeatherForecast> Get()//un fel de lista din java, dar mai general fiind ca orice se poate genera, "i"-interfata
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast//genereaza nr intre 1 si 5 inclusiv si din astrea pt fiecare cu seelct creaza un obiect de tip forecast la care 
            {
                Date = DateTime.Now.AddDays(index),//date-ul e data de azi +atatea zile cat e indexul
                TemperatureC = rng.Next(-20, 55),//genereaza random intre
                Summary = Summaries[rng.Next(Summaries.Length)]// summary random sin lisat
            })
            .ToArray();//face array si returneaza
        }
    }
}
