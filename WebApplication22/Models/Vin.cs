using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class Vin
    {
        public int ID { get; set; }
        public string VIN { get; set; }
        public int ModelYearId { get; set; }

        public ModelYear ModelYear { get; set; }
    }
}