using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class ModelYear
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public object CarDetailsJSON { get; set; }
        public object ScheduleJSON { get; set; }
        public int ModelID { get; set; }
        public int VpicID { get; set; }

        public Model Model { get; set; }
    }
}