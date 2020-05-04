using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class CarController : ApiController
    {
        CarDbContext db = new CarDbContext("Server=localhost; Port=3306;  uid=root; Pwd=12345; Database=mydb; charset=utf8;");

        [HttpGet]
        public object Vin(string vin)
        {
            Vin _vin = db.Get<Vin>().FirstOrDefault(v => v.VIN == vin);
            return new { code = 200, result = new { carMake = _vin.ModelYear.Model.Make.Name, carModel = _vin.ModelYear.Model.Name } };
        }

        [HttpGet]
        public object Services()
        {
            List<Service> services = db.Get<Service>();
            return new { code = 200, result = services.Select(s => new { name = s.Name, id = s.Id }).ToArray() };
        }

        [HttpGet]
        public object Spares(int serviceId)
        {
            List<Spare> spares = db.Get<Spare>().Where(s => s.ServiceId == serviceId).ToList();
            return new { code = 200, result = spares.Select(s => new { name = s.Name, costOrigin = s.CostOrigin, costReplacement = s.CostReplacement, replacementProduction = s.ReplacementProduction, originDuration = s.OriginDuration, replacementDuration = s.ReplacementDuration }).ToArray() };
        }

        [HttpGet]
        public object Makes()
        {
            List<Make> makes = db.Get<Make>();
            return new { code = 200, result = makes.Select(m => new { make = m.Name }).ToArray() };
    }

    [HttpPost]
        public object AddMake(Make make)
        {
            db.Insert(make);

            return new { code = 200, result = new { mode = make.Name } };
        }

        [HttpGet]
        public object Models()
        {
            List<Model> models = db.Get<Model>();
            return new { code = 200, result = models.Select(m => new { model = m.Name }).ToArray() };
        }

        [HttpPost]
        public object AddModel(Model model)
        {
            db.Insert(model);

            return new { code = 200, result = new { mode = model.Name } };
        }

        [HttpGet]
        public object ScheduleRequests()
        {
            List<ScheduleRequest> scheduleRequests = db.Get<ScheduleRequest>();
            return new { code = 200, result = scheduleRequests.Select(sr => new { id = sr.ID, time = sr.Time }).ToArray() };
        }

        [HttpPost]
        public object Application(ApplicationModel model)
        {
            Application app = new Application
            {
                Date = model.Date,
                Phone = model.Phone,
                Email = model.Email
            };

            db.Insert(app);
            int appId = db.Get<Application>().Last().Id;

            model.Spares.ForEach(s => {
                s.ApplicationId = appId;
                db.Insert(s);
            });

            db.Insert(model.Schedule);

            return new { code = 200 };
        }
    }
}
