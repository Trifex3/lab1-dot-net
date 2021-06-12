using System;

namespace Project1
{
    public class WeatherForecast// o cls unde avem propreitati
    {
        public DateTime Date { get; set; }//getter si setter; implementari default; implememtari daca dupa set/get pun {}

        public int TemperatureC { get; set; }//daca vreau sa fac read-only sterg set-ul
                                             // daca pun init in loc de constructor

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
