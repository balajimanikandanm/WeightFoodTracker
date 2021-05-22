using System;
using Newtonsoft.Json;

namespace ConsumerAPITests.Models
{
    public class Consumer
    {
        public int ConsumerId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        public int Weight { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int BreakFastId { get; set; }

        public int LunchId { get; set; }

        public int DinnerId { get; set; }

        public int Calories { get; set; }

        public override string ToString()
        {
            return  JsonConvert.SerializeObject(this);
        }

        public static bool IsConsumerValid(Consumer ns, out string errMsg)
        {
            bool res = IsConsumerNameValid(ns.Name, out errMsg);
            if(!res)
            {
                return false;
            }

            errMsg = "";
            return true;
        }

        public static bool IsConsumerNameValid(string consumerName, out string errMsg)
        {
            if (consumerName.Length <= 1 || consumerName.Trim().Length <= 1)
            {
                errMsg = "Name cannot be empty. Please input a name";
                return false;
            }
            errMsg = "";
            return true;
        }

        public static bool IsConsumerIdValid(string strConsumerId, out int consumerId, out string errMsg)
        {
            // ******************************************************
            // Validation
            // Check ConsumerId is not string, > 0 && < 999
            // ******************************************************
            bool res = Int32.TryParse(strConsumerId, out consumerId);
            if (!res)  //res == false  //res == true
            {
                errMsg = "Invalid Input. Please input a valid ConsumerId";
                return false;
            }

            // Check if ConsumerId > 0
            if (consumerId <= (int) ConsumerValue.MinValue)
            {
                errMsg = $"Consumer Id should be greater than {(int) ConsumerValue.MinValue}";
                return false;
            }

            if (consumerId > (int) ConsumerValue.MaxValue)
            {
                errMsg = $"Consumer Id should be less than {(int) ConsumerValue.MaxValue}";
                return false;
            }
            errMsg = "";
            return true;
        }
    }

}
