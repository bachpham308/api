using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class ScheduleRequest
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public int VINID { get; set; }
        public int Mileage { get; set; }
        public string Time { get; set; }
    }
}