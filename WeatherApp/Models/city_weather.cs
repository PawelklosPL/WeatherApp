//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class city_weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public Nullable<double> temp { get; set; }
        public Nullable<double> pressur { get; set; }
        public Nullable<double> humidity { get; set; }
        public Nullable<double> speed { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> last_update_time { get; set; }
    }
}
