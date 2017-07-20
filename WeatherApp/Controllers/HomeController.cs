using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeatherApp.Models;
using System.Net.Http.Formatting;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        /* Database weather object */
        private DBweatherEntities db = new DBweatherEntities();

        /* Method - Inssert value to DB */
        public static string InsertToDB(OpenWeather w)
        {
            try
            {
                /* Connection string to DB */
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-SV3DAJP;Initial Catalog=DBweather;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                {
                    /* Building a query */
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("InsertWeather", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@main", w.weather[0].main));
                    cmd.Parameters.Add(new SqlParameter("@description", w.weather[0].description));
                    cmd.Parameters.Add(new SqlParameter("@icon", w.weather[0].icon));
                    cmd.Parameters.Add(new SqlParameter("@temp", Convert.ToDouble(w.main.temp.Replace(".", ",")) - 272.15));
                    cmd.Parameters.Add(new SqlParameter("@pressur", w.main.pressure));
                    cmd.Parameters.Add(new SqlParameter("@humidity", w.main.humidity));
                    cmd.Parameters.Add(new SqlParameter("@speed", w.wind.speed));
                    cmd.Parameters.Add(new SqlParameter("@country", w.sys.country));
                    cmd.Parameters.Add(new SqlParameter("@name", w.name));

                    /* Execute query */
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        return null;
                    }
                }
            }
            catch (Exception exception)
            {
                /* "Logs" execptions */
                Debug.WriteLine(exception.ToString());
                return exception.ToString();
            } 

        }
        /* Method - Get Value from web service */
        /* Apli name - openweathermap */
        /* Api url - https://openweathermap.org/ */
        private static async Task GetPageSizeAsync(string city_name)  
       {
            /* Create uri and message */
            var client = new RestClient("http://api.openweathermap.org/data/2.5/weather?q=" + city_name + "&APPID=1a5898dc452ba792f5d8efc82513a471");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "2fbb3c2e-97e4-c33d-7705-3e41c75b82f7");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            /* Send request */
            IRestResponse response = client.Execute(request);
                /* If status is OK "Log" value and Insert to DB */
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    /* Create DB object using donwloads body */
                    OpenWeather weather = JsonConvert.DeserializeObject<OpenWeather>(response.Content);
                    //Debug.WriteLine("temp: " + weather.main.temp);
                    //Debug.WriteLine("pressure: " + weather.main.pressure);
                    //Debug.WriteLine("humidity: " + weather.main.humidity);
                    //Debug.WriteLine("description: " + weather.weather[0].description);
                    //Debug.WriteLine("icon: " + weather.weather[0].icon);
                    //Debug.WriteLine("main: " + weather.weather[0].main);
                    //Debug.WriteLine("speed: " + weather.wind.speed);
                    //Debug.WriteLine("country: " + weather.sys.country);
                    //Debug.WriteLine("name: " + weather.name);
                    /* Go to insert value method */
                    InsertToDB(weather);
               }
       }
        /* Home Page, Welcome Page */
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        // GET: /Home/User
        /* User Controler */
        public ActionResult User()
        {
            /* Create list from DB model and return to View */
            //Debug.WriteLine(db.city_weather.);
            return View(db.city_weather.ToList());
        }
        // POST: /Home/InsertWeather
        /* Admin Controler - If post value "name" go to Donwload web service data */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertWeather([Bind(Include = "name")] city_weather city_weather)
        {
           /* Take Post value and go to donwload web service data */
           GetPageSizeAsync(city_weather.name);
           return View(city_weather);
        }
        /* Admin Controler - if no value in post */
        // GET: /Home/InsertWeather
        public ActionResult InsertWeather()
        {
            return View();
        }
    }
}
