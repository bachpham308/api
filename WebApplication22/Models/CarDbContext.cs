namespace WebApplication22.Models
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;

    public class CarDbContext
    {
        private readonly string connectionString;
        private readonly MySqlConnection connection;

        public CarDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            this.connection = new MySqlConnection(connectionString);
        }

        public void Insert<T>(T entity) where T : class
        {
            connection.Open();

            string commandText = GetInsertIntoCommandText(entity);
            MySqlCommand cmd = new MySqlCommand(commandText, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete<T>(T entity) where T : class
        {
            connection.Open();

            string commandText = GetDeleteCommandText(entity);
            MySqlCommand cmd = new MySqlCommand(commandText, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public List<T> Get<T>(bool isLazy = false) where T : class
        {
            List<T> entities = new List<T>();

            connection.Open();

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * from {GetTableName<T>()}";
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                entities.Add(CreateEntity<T>(reader, isLazy));
            }

            reader.Close();
            connection.Close();

            return entities;
        }

        private string GetTableName<T>() where T : class
        {
            if (typeof(T).Equals(typeof(User)))
            {
                return "user";
            }
            else if (typeof(T).Equals(typeof(Make)))
            {
                return "make";
            }
            else if (typeof(T).Equals(typeof(Model)))
            {
                return "model";
            }
            else if (typeof(T).Equals(typeof(ModelYear)))
            {
                return "modelyear";
            }
            else if (typeof(T).Equals(typeof(ScheduleRequest)))
            {
                return "ScheduleRequest";
            }
            else if (typeof(T).Equals(typeof(Vin)))
            {
                return "Vin";
            }
            else if (typeof(T).Equals(typeof(Spare)))
            {
                return "Spare";
            }
            else if (typeof(T).Equals(typeof(Application)))
            {
                return "Application";
            }
            else if (typeof(T).Equals(typeof(Service)))
            {
                return "Service";
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private string GetInsertIntoCommandText<T>(T entity)
        {
            string commandText = null;
            if (typeof(T).Equals(typeof(Make)))
            {
                Make make = entity as Make;
                commandText = $"INSERT INTO Make(Name, VpicID) VALUES ('{make.Name}', {make.VpicID})";
            }
            else if (typeof(T).Equals(typeof(Model)))
            {
                Model model = entity as Model;
                commandText = $"INSERT INTO Model(ID, Name, TrimName, MakeID, VpicID) VALUES ({model.ID}, '{model.Name}', '{model.TrimName}', {model.MakeID}, {model.VpicID})";
            }
            else if (typeof(T).Equals(typeof(ScheduleRequest)))
            {
                ScheduleRequest sr = entity as ScheduleRequest;
                commandText = $"INSERT INTO schedulerequest(IP, VINID, Mileage, time)VALUES('{sr.IP}',{sr.VINID},{sr.Mileage},'{sr.Time}')";
            }
            else if (typeof(T).Equals(typeof(Spare)))
            {
                Spare spare = entity as Spare;
                commandText = $"INSERT INTO spare(name, costOrigin, costReplacement, replacementProduction, originDuration, replacementDuration, serviceId, applicationId) VALUES('{spare.Name}',{spare.CostOrigin},{spare.CostReplacement},'{spare.ReplacementProduction}',{spare.OriginDuration},{spare.ReplacementDuration},{spare.ServiceId},{spare.ApplicationId})";
            }
            else if (typeof(T).Equals(typeof(Application)))
            {
                Application app = entity as Application;
                commandText = $"INSERT INTO application(date, phone, email)VALUES('{app.Date}','{app.Phone}','{app.Email}')";
            }

            return commandText;
        }
        private string GetDeleteCommandText<T>(T entity)
        {
            string commandText = null;
            if (typeof(T).Equals(typeof(User)))
            {
            }

            return commandText;
        }


        private T CreateEntity<T>(MySqlDataReader reader, bool isLazy = false) where T : class
        {
            T entity = null;
            if (typeof(T).Equals(typeof(User)))
            {
                int id = (int)reader["id"];
                string name = (string)reader["name"];
                string phone = (string)reader["phone"];

                CarDbContext db = new CarDbContext(connectionString);

                entity = new User
                {
                    Id = id,
                    Name = name,
                    Phone = phone
                } as T;
            }
            else if (typeof(T).Equals(typeof(Make)))
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];
                int VpicID = (int)reader["VpicID"];

                CarDbContext db = new CarDbContext(connectionString);

                entity = new Make
                {
                    ID = id,
                    Name = name,
                    VpicID = VpicID
                } as T;
            }
            else if (typeof(T).Equals(typeof(Model)))
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];
                string trimName = (string)reader["TrimName"];
                int MakeID = (int)reader["MakeID"];
                int VpicID = (int)reader["VpicID"];

                CarDbContext db = new CarDbContext(connectionString);
                Make make = db.Get<Make>(true).FirstOrDefault(m => m.ID == MakeID);

                entity = new Model
                {
                    ID = id,
                    Name = name,
                    TrimName = trimName,
                    MakeID = MakeID,
                    VpicID = VpicID,

                    Make = make
                } as T;
            }
            else if (typeof(T).Equals(typeof(ScheduleRequest)))
            {
                int id = (int)reader["ID"];
                string IP = (string)reader["IP"];
                int VINID = (int)reader["VINID"];
                int Mileage = (int)reader["Mileage"];
                string time = (string)reader["time"];

                CarDbContext db = new CarDbContext(connectionString);

                entity = new ScheduleRequest
                {
                    ID = id,
                    IP = IP,
                    VINID = VINID,
                    Mileage = Mileage,
                    Time = time
                } as T;
            }
            else if (typeof(T).Equals(typeof(Vin)))
            {
                int id = (int)reader["ID"];
                string VIN = (string)reader["VIN"];
                int ModelYearId = (int)reader["ModelYearId"];

                CarDbContext db = new CarDbContext(connectionString);
                ModelYear modelYear = db.Get<ModelYear>(true).FirstOrDefault(m => m.ID == ModelYearId);

                entity = new Vin
                {
                    ID = id,
                    VIN = VIN,
                    ModelYearId = ModelYearId,
                    ModelYear = modelYear
                } as T;
            }
            else if (typeof(T).Equals(typeof(ModelYear)))
            {
                int ID = (int)reader["ID"];
                int Year = (int)reader["Year"];
                object CarDetailsJSON = reader["CarDetailsJSON"];
                object ScheduleJSON = reader["ScheduleJSON"];
                int ModelID = (int)reader["ModelID"];
                int VpicID = (int)reader["VpicID"];

                CarDbContext db = new CarDbContext(connectionString);
                Model model = db.Get<Model>(true).FirstOrDefault(m => m.ID == ModelID);

                entity = new ModelYear
                {
                    ID = ID,
                    Year = Year,
                    CarDetailsJSON = CarDetailsJSON,
                    ScheduleJSON = ScheduleJSON,
                    ModelID = ModelID,
                    VpicID = VpicID,
                    Model = model
                } as T;
            }
            else if (typeof(T).Equals(typeof(Service)))
            {
                int id = (int)reader["id"];
                string name = (string)reader["name"];
                int modelId = (int)reader["modelId"];

                CarDbContext db = new CarDbContext(connectionString);
                Model model = db.Get<Model>(true).FirstOrDefault(m => m.ID == modelId);

                entity = new Service
                {
                    Id = id,
                    Name = name,
                    ModelId = modelId,
                    Model = model
                } as T;
            }
            else if (typeof(T).Equals(typeof(Application)))
            {
                int id = (int)reader["id"];
                string Date = (string)reader["Date"];
                string Phone = (string)reader["Phone"];
                string Email = (string)reader["Email"];

                CarDbContext db = new CarDbContext(connectionString);

                entity = new Application
                {
                    Id = id,
                    Date = Date,
                    Phone = Phone,
                    Email = Email
                } as T;
            }
            else if (typeof(T).Equals(typeof(Spare)))
            {
                int id = (int)reader["id"];
                string name = (string)reader["name"];
                int costOrigin = (int)reader["costOrigin"];
                int costReplacement = (int)reader["costReplacement"];
                string replacementProduction = (string)reader["replacementProduction"];
                int originDuration = (int)reader["originDuration"];
                int replacementDuration = (int)reader["replacementDuration"];
                int serviceId = (int)reader["serviceId"];
                int applicationId = (int)reader["applicationId"];

                CarDbContext db = new CarDbContext(connectionString);
                Service service = db.Get<Service>(true).FirstOrDefault(s => s.Id == serviceId);
                Application application = db.Get<Application>(true).FirstOrDefault(app => app.Id == applicationId);

                entity = new Spare
                {
                    Id = id,
                    Name = name,
                    CostOrigin = costOrigin,
                    CostReplacement = costReplacement,
                    ReplacementProduction = replacementProduction,
                    OriginDuration = originDuration,
                    ReplacementDuration = replacementDuration,
                    ServiceId = serviceId,
                    ApplicationId = applicationId,
                    Service = service,
                    Application = application
                } as T;
            }
            else
            {
                throw new NotSupportedException();
            }

            return entity;
        }
    }
}