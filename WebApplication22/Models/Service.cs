using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }

        public Model Model { get; set; }
    }
}