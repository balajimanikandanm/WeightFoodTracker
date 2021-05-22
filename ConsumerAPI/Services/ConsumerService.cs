using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using ConsumerAPI.Models;


namespace ConsumerAPI.Services
{
    public class ConsumerService
    {
        string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=FoodWeightDB;Integrated Security=True;";
        public Consumer GetConsumer(int consumerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                string sqlStmt = String.Format("Select ConsumerId, Name,Age,DOB, Gender,Weight,Email,Address,BreakFastId,LunchId,DinnerId,Calories  from Consumer Where ConsumerId ={0}", consumerId);
                //string sqlStmt1 = "Select ConsumerId, Name, Gender, DOB from Consumer Where ConsumerId = " + ConsumerId;
                //string sqlStmt2 = $"Select ConsumerId, Name, Gender, DOB from Consumer Where ConsumerId = {ConsumerId}";
                Console.WriteLine(sqlStmt);
                //Console.WriteLine(sqlStmt1);
                //Console.WriteLine(sqlStmt2);

                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Consumer s = new Consumer();
                            s.ConsumerId = reader.GetInt32(0);
                            s.Name = reader.GetString(1);
                            s.Age = reader.GetInt32(2);
                            s.DOB = reader.GetDateTime(3);
                            s.Gender = reader.GetString(4);
                            s.Weight = reader.GetInt32(5);
                            s.Email = reader.GetString(6);
                            s.Address = reader.GetString(7);
                            s.BreakFastId = reader.GetInt32(8);
                            s.LunchId = reader.GetInt32(9);
                            s.DinnerId = reader.GetInt32(10);
                            s.Calories = reader.GetInt32(11);



                            // Console.WriteLine("before new");
                            // Consumer s = new Consumer(reader.GetInt32(0),
                            //                 reader.GetString(1),
                            //                 reader.GetChar(2),
                            //                 reader.GetDateTime(3));

                            Console.WriteLine(s.Name);
                            return s;
                        }
                    }
                }
                return null;
            }
        }

        public List<Consumer> GetAllConsumers()
        {
            List<Consumer> consumers = new List<Consumer>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string sqlStmt = String.Format("Select ConsumerId, Name,Age,DOB, Gender,Weight, Email,Address,BreakFastId,LunchId,DinnerId,Calories  from Consumer");

                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Consumer s = new Consumer();
                            s.ConsumerId = reader.GetInt32(0);
                            s.Name = reader.GetString(1);
                            s.Age = reader.GetInt32(2);
                            s.DOB = reader.GetDateTime(3);
                            s.Gender = reader.GetString(4);
                            s.Weight = reader.GetInt32(5);
                            s.Email = reader.GetString(6);
                            s.Address = reader.GetString(7);
                            s.BreakFastId = reader.GetInt32(8);
                            s.LunchId = reader.GetInt32(9);
                            s.DinnerId = reader.GetInt32(10);
                            s.Calories = reader.GetInt32(11);



                            consumers.Add(s);
                        }
                    }
                }
                return consumers;
            }
        }

        public int InsertConsumer(Consumer newConsumer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                // string sqlStmt = String.Format(
                // "INSERT INTO [dbo].[Consumer] ([ConsumerId],[Name],[DOB],[Gender], [City], [State]) VALUES ({0},'{1}','{2}', '{3}', '{4}', '{5}')",
                //  newConsumer.ConsumerId, newConsumer.Name, newConsumer.DOB, newConsumer.Gender,
                //  newConsumer.City, newConsumer.State);

                //string sqlStmt = $"INSERT INTO [dbo].[Consumer] ([ConsumerId],[Name],[DOB],[Gender], [City], [State]) VALUES ({newConsumer.ConsumerId},'{newConsumer.Name}','{newConsumer.DOB}', '{newConsumer.Gender}', '{newConsumer.City}', '{newConsumer.State}')";

                string sqlStmt = $"INSERT INTO [dbo].[Consumer] ([Name],[Age],[DOB], [Gender],[Weight], [Email],[Address],[BreakFastId],[LunchId],[DinnerId],[Calories]) OUTPUT INSERTED.ConsumerId VALUES ('{newConsumer.Name}','{newConsumer.Age}','{newConsumer.DOB.ToString("yyyy-MM-dd")}', '{newConsumer.Gender}','{newConsumer.Weight}','{newConsumer.Email}','{newConsumer.Address}','{newConsumer.BreakFastId}','{newConsumer.LunchId}','{newConsumer.DinnerId}','{newConsumer.Calories}')";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int newConsumerId = (int)command.ExecuteScalar();
                    //int numOfRows = command.ExecuteNonQuery();
                    return newConsumerId;
                }
            }
        }

        public int UpdateConsumer(Consumer updConsumer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                string sqlStmt = @$"UPDATE [dbo].[Consumer] SET
                    Name = '{updConsumer.Name}',
                    Age = '{updConsumer.Age}',
                    DOB = '{updConsumer.DOB.ToString("yyyy-MM-dd")}',
                    Gender = '{updConsumer.Gender}',
                    Weight = '{updConsumer.Weight}',
                    Email = '{updConsumer.Email}',
                    Address = '{updConsumer.Address}',
                    BreakFastId = '{updConsumer.BreakFastId}',
                    LunchId = '{updConsumer.LunchId}',
                    DinnerId = '{updConsumer.DinnerId}',
                    Calories = '{updConsumer.Calories}'

                    Where ConsumerId = {updConsumer.ConsumerId}";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int numOfRows = command.ExecuteNonQuery();
                    return numOfRows;
                }
            }
        }

        public bool DeleteConsumer(int consumerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open Connection
                con.Open();

                // Get SQL Statement
                string sqlStmt = $"DELETE FROM [dbo].[Consumer] WHERE ConsumerId = {consumerId}";

                Console.WriteLine(sqlStmt);

                // Execute Statement
                using (SqlCommand command = new SqlCommand(sqlStmt, con))
                {
                    int numOfRows = command.ExecuteNonQuery();
                    if (numOfRows >0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}