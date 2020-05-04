using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication22.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        CarDbContext db = new CarDbContext("Server=localhost; Port=3306;  uid=root; Pwd=123456; Database=mydb; charset=utf8;");

        [TestMethod]
        public void TestMethod1()
        {
            List<Vin> vins = db.Get<Vin>();
        }
    }
}
