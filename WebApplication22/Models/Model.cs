using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TrimName { get; set; }
        public int MakeID { get; set; }
        public int VpicID { get; set; }

        public Make Make { get; set; }
    }
}