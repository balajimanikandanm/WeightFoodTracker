using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConsumerAPI.Models;
using ConsumerAPI.Services;


namespace ConsumerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumersController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                ConsumerService abcd = new ConsumerService();
                List<Consumer> consumers = abcd.GetAllConsumers();
                if(consumers.Count <= 0)
                {
                    return NotFound("No consumers exist");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(consumers);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        [HttpGet("{id}")]
        //[HttpGet]
        public IActionResult Get([FromRoute] string id)
        {
            try
            {
                // ******************************************************
                // 1. Validation
                // ******************************************************
                bool res = Consumer.IsConsumerIdValid(id, out int consumerId, out string errMsg);
                if (!res)  //res == false  //res == true
                {
                    BadRequest(errMsg);
                }

                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                ConsumerService ConsumerService = new ConsumerService();
                Consumer consumer = ConsumerService.GetConsumer(consumerId);
                if(consumer == null)
                {
                    return NotFound("Consumer with Consumer Id - " + consumerId + " does not exist.");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(consumer);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Consumer updConsumer)
        {
            try
            {
                //1. Validation
                bool res = Consumer.IsConsumerValid(updConsumer, out string errMsg);
                if (res == false)  //!res
                {
                    return BadRequest(errMsg);
                }

                //2. Execute DB
                ConsumerService sda = new ConsumerService();
                int numRows = sda.UpdateConsumer(updConsumer);
                if (numRows == 0)
                {
                    return BadRequest("Invalid Consumer. Cannot Insert.");
                }
                //3. Return Data
                return Ok(updConsumer);
            }
            catch(Exception ex)
            {
                // 4. If Exception return 500
                LogError(ex);
                return StatusCode(500, "Unknown Error - " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Consumer newConsumer)
        {
            try
            {
                //1. Validation
                bool res = Consumer.IsConsumerValid(newConsumer, out string errMsg);
                if (res == false)  //!res
                {
                    return BadRequest(errMsg);
                }

                //2. Execute DB
                ConsumerService sda = new ConsumerService();
                // int numOfRows = sda.InsertConsumer(newConsumer);
                int newConsumerId = sda.InsertConsumer(newConsumer);
                if (newConsumerId <= 0)
                {
                    return BadRequest("Invalid Consumer. Cannot Insert.");
                }
                //3. Return Data
                newConsumer.ConsumerId = newConsumerId;
                return Ok(newConsumer);
            }
            catch(Exception ex)
            {
                // 4. If Exception return 500
                LogError(ex);
                return StatusCode(500, "Unknown Error - " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]string id)
        {
            try
            {
                // ******************************************************
                // 1. Validation
                // ******************************************************
                bool res = Consumer.IsConsumerIdValid(id, out int consumerId, out string errMsg);
                if (!res)  //res == false  //res == true
                {
                    BadRequest(errMsg);
                }

                // ******************************************************
                // 2. Run SQL statement
                // ******************************************************
                ConsumerService ConsumerService = new ConsumerService();
                bool numOfRows = ConsumerService.DeleteConsumer(consumerId);
                if(numOfRows == false)
                {
                    return NotFound("Consumer with Consumer Id - " + consumerId + " does not exist.");
                }

                // ******************************************************
                // 3. Return Data
                // ******************************************************
                return Ok(consumerId);
            }
            catch (Exception ex)
            {
                // ******************************************************
                // 4. If Exception return 500
                // ******************************************************
                LogError(ex);
                return StatusCode(500, "Unknown Error");
            }
        }

        private void LogError(Exception ex)
        {
            //Do Something to Log an Error
        }
    }
}
