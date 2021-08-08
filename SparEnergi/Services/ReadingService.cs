using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using SparEnergi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;

namespace SparEnergi.Services
{
    public class ReadingService
    {
        List<ReadingModel> readings = new List<ReadingModel>();
        
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = SparEnergiDb; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int RegisterNewReading(ReadingModel readingModel, int UserId)
        {
            DateTime date = DateTime.Today;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlStatement = "INSERT INTO dbo.Readings (Date, UserId, EnergyUsed, EnergyUnit, WaterUsed, WaterUnit) VALUES (@Date, @UserId, @EnergyUsed, @EnergyUnit, @WaterUsed, @WaterUnit)";

                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Date", System.Data.SqlDbType.Date).Value = date;
                command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;
                command.Parameters.Add("@EnergyUsed", System.Data.SqlDbType.Int).Value = readingModel.EnergyUsed;
                command.Parameters.Add("@EnergyUnit", System.Data.SqlDbType.NChar).Value = "kWh";
                command.Parameters.Add("@WaterUsed", System.Data.SqlDbType.Int).Value = readingModel.WaterUsed;
                command.Parameters.Add("@WaterUnit", System.Data.SqlDbType.NChar).Value = "m3";

                try
                {
                    connection.Open();
                    return (command.ExecuteNonQuery());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

        }

        public bool RegisterReadingsFromFile(String filePath)
        {
            DateTime date = DateTime.Today;
            bool finished = false;
            var csvTable = new DataTable();

            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filePath))))
            {
                csvTable.Load(csvReader);
            }

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                string dateTime = csvTable.Rows[i][0].ToString();
                int userId = Convert.ToInt32(csvTable.Rows[i][1]);
                string energyUsed = csvTable.Rows[i][2].ToString();
                string energyUnit = csvTable.Rows[i][3].ToString();
                string waterUsed = csvTable.Rows[i][4].ToString();
                string waterUnit = csvTable.Rows[i][5].ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                
                    string sqlStatement = "INSERT INTO [dbo].[Readings] (Date, UserId, EnergyUsed, EnergyUnit, WaterUsed, WaterUnit) VALUES (@Date, @UserId, @EnergyUsed, @EnergyUnit, @WaterUsed, @WaterUnit)";

                    SqlCommand command = new SqlCommand(sqlStatement, connection);

                    command.Parameters.Add("@Date", System.Data.SqlDbType.Date).Value = dateTime;
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = userId;
                    command.Parameters.Add("@EnergyUsed", System.Data.SqlDbType.Int).Value = energyUsed;
                    command.Parameters.Add("@EnergyUnit", System.Data.SqlDbType.NChar).Value = energyUnit;
                    command.Parameters.Add("@WaterUsed", System.Data.SqlDbType.Int).Value = waterUsed;
                    command.Parameters.Add("@WaterUnit", System.Data.SqlDbType.NChar).Value = waterUnit;

                    try
                    {
                        connection.Open();
                        command.ExecuteReader();
                        finished = true;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            File.Delete(filePath);
            return finished;
        }



        public List<ReadingModel> FetchReadings(int UserId)
        {
            List<ReadingModel> readingModelsArray = new List<ReadingModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();  

                if(readingModelsArray.Count > 0)
                {
                    readings.Clear();
                }
                try
                {
                                      
                    string sqlStatement = "SELECT * FROM [dbo].[Readings] WHERE UserId = @UserId ";                    
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    command.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = UserId;
                    //dataReader = command.ExecuteReader();
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            readingModelsArray.Add(new ReadingModel()
                            {
                                Date = Convert.ToDateTime(reader["Date"])
                            ,
                                UserId = Convert.ToInt32(reader["UserId"])
                            ,
                                EnergyUsed = Convert.ToInt32(reader["EnergyUsed"])
                            ,
                                EnergyUnit = reader["EnergyUnit"].ToString()
                            ,
                                WaterUsed = Convert.ToInt32(reader["WaterUsed"])
                            ,
                                WaterUnit = reader["WaterUnit"].ToString()
                            });
                        }

                    }
                    
                    return readingModelsArray;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return null;
                }
            }            
        }
        
    }
}
