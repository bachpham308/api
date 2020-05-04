using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class Spare
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public int CostOrigin { get; set; }
        public int CostReplacement { get; set; }
        public string ReplacementProduction { get; set; }
        public int OriginDuration { get; set; }
        public int ReplacementDuration { get; set; }
        public int ServiceId { get; set; }
        public int ApplicationId { get; set; }

        public Service Service { get; set; }
        public Application Application { get; set; }
    }
}