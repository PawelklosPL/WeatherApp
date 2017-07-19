using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp
{
    public class OpenWeather
    {
      //  public Tweather weather { get; set; }
        public Tmain main { get; set; }
        public Twind wind { get; set; }
        public Tsys sys { get; set; }
        public List<Tweather> weather { get; set; }
        public string name { get; set; }

    }
    public class Tmain
    {
        public string temp { get; set; }
        public string pressure { get; set; }
        public string humidity { get; set; }
    }
    public class Twind
    {
        public string speed { get; set; }
    }
    public class Tsys
    {
        public string country { get; set; }
    }
    public class Tweather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

}