using System;
using NUnit.Framework;
using Newtonsoft.Json;
using ConsumerAPITests.Models;
using System.Globalization;
using ConsumerAPI.Services;

    

namespace ConsumerAPITests
{
   
    [TestFixture]
    public class ConsumerTests
    {
        private ConsumerAPIHelper apiHelper;
        [SetUp]
        public void Setup()
        {
            apiHelper = new ConsumerAPIHelper();
        }

        [Test]
        [TestCase("admin", "admin123")]
        [TestCase("user", "user123")]

        public void LoginTest(string userName, string password)
        {
            var svc = new UserService();
            var res = svc.Login(userName, password, out string token, out string errMsg);
            Assert.IsTrue(res);
            Assert.IsNotNull(token);
            Assert.AreEqual(errMsg, "");
        }

        [Test]
        [TestCase("admin", "a123")]
        [TestCase("user", "u123")]

        public void LoginFailTest(string userName, string password)
        {
            var svc = new UserService();
            var res = svc.Login(userName, password, out string token, out string errMsg);
            Assert.IsFalse(res);
            Assert.AreEqual(token, "");
            Assert.IsNotNull(errMsg);
        }


        [Test]
        public void GetConsumersTest()
        {
            var consumers = apiHelper.GetConsumersAsync().Result;
            Console.WriteLine(JsonConvert.SerializeObject(consumers));
            Assert.IsNotNull(consumers);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void GetConsumerTest(int consumerId)
        {
            var consumer = apiHelper.GetConsumerAsync(consumerId).Result;
            Assert.IsNotNull(consumer);
            Assert.IsNotNull(consumer.Name);
        }

        [Test]
        public void InsertconsumerTest()
        {
            var newConsumer = new Consumer();
            newConsumer.Name = "Ramesh";
            newConsumer.Age = 20;
            newConsumer.DOB = DateTime.ParseExact("2000-05-20", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            newConsumer.Gender = "M";
            newConsumer.Weight = 70;
            newConsumer.Email = "rr@gmail.com";
            newConsumer.Address = "Tiruchi";
            newConsumer.BreakFastId = 2;
            newConsumer.LunchId = 2;
            newConsumer.DinnerId = 2;
            newConsumer.Calories = 56;





            var insConsumer = apiHelper.CreateConsumerAsync(newConsumer).Result;
            Assert.IsNotNull(insConsumer);
            Assert.Greater(insConsumer.ConsumerId, 0);

            //Get Consumer and Validate
            //GetConsumerTest(insConsumer.ConsumerId);
            var consumer = apiHelper.GetConsumerAsync(insConsumer.ConsumerId).Result;
            Assert.IsNotNull(consumer);
            Assert.IsNotNull(consumer.Name);

            Assert.AreEqual(newConsumer.Name, consumer.Name);

            //Updated Consumer
            var updConsumer = new Consumer();
            updConsumer.Name = "Ramesh";
            updConsumer.Age = 20;
            updConsumer.DOB = DateTime.ParseExact("2000-05-20", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            updConsumer.Gender = "M";
            updConsumer.Weight = 70;
            updConsumer.Email = "rr@gmail.com";
            updConsumer.Address = "Tiruchi";
            updConsumer.BreakFastId = 2;
            updConsumer.LunchId = 2;
            updConsumer.DinnerId = 2;
            updConsumer.Calories = 56;
            updConsumer.ConsumerId = insConsumer.ConsumerId;

            var updatedConsumer = apiHelper.UpdateConsumerAsync(updConsumer).Result;
            Assert.IsNotNull(updatedConsumer);
            Assert.AreEqual(updatedConsumer.Name, updatedConsumer.Name);

            consumer = apiHelper.GetConsumerAsync(insConsumer.ConsumerId).Result;
            Assert.IsNotNull(consumer);

            //Delete consumer
            var delConsumerId = apiHelper.DeleteConsumerAsync(insConsumer.ConsumerId).Result;
            Assert.AreEqual(insConsumer.ConsumerId, delConsumerId);

            consumer = apiHelper.GetConsumerAsync(insConsumer.ConsumerId).Result;
            Assert.IsNull(consumer);
        }
    }
}
     
