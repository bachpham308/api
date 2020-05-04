using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class ApplicationModel
    {
        public List<Spare> Spares { get; set; }
        public ScheduleRequest Schedule { get; set; }
        public string Date { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}