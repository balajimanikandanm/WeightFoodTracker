using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using ConsumerAPITests.Models;

    

namespace ConsumerAPITests
{
     
    
    
    public class ConsumerAPIHelper
    {
        HttpClient client = new HttpClient();
        private string url = "http://localhost:5000/consumers/";

        public void ShowConsumer(Consumer consumer)
        {
            Console.WriteLine($"Name: {consumer.Name}\tConsumer Id: " +
                $"{consumer.ConsumerId}\tGender: {consumer.Gender}");
        }

        public async Task<Consumer> CreateConsumerAsync(Consumer newConsumer)
        {
            var content = new StringContent(newConsumer.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}",  content);
            var consumerData = await response.Content.ReadAsStringAsync();
            var consumer = JsonConvert.DeserializeObject<Consumer>(consumerData);
            return consumer;
        }

        public async Task<Consumer> GetConsumerAsync(int id)
        {
            Consumer consumer = null;
            HttpResponseMessage response = await client.GetAsync($"{url}{id}");
            if (response.IsSuccessStatusCode)
            {
                //consumer = await response.Content.ReadAsAsync<Consumer>();
                var consumerStrData = await response.Content.ReadAsStringAsync();
                consumer = JsonConvert.DeserializeObject<Consumer>(consumerStrData);
            }
            return consumer;
        }

        public async Task<List<Consumer>> GetConsumersAsync()
        {
            List<Consumer> consumers = null;
            HttpResponseMessage response = await client.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                //consumer = await response.Content.ReadAsAsync<Consumer>();
                var consumerStrData = await response.Content.ReadAsStringAsync();
                consumers = JsonConvert.DeserializeObject<List<Consumer>>(consumerStrData);
            }
            return consumers;
        }

        public async Task<Consumer> UpdateConsumerAsync(Consumer updConsumer)
        {
            var content = new StringContent(updConsumer.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{url}", content);
            var consumerStrData = await response.Content.ReadAsStringAsync();
            var consumerData = JsonConvert.DeserializeObject<Consumer>(consumerStrData);
            return consumerData;
        }

        public async Task<int> DeleteConsumerAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{url}{id}");
            var delData = await response.Content.ReadAsStringAsync();
            var numOfRows = int.Parse(delData);
            return numOfRows;
        }
    }
   
}