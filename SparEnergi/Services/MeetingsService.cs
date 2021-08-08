using Microsoft.Data.SqlClient;
using SparEnergi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparEnergi.Services
{
    public class MeetingsService
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = SparEnergiDb; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public int RegisterNewMeeting(MeetingModel meeting)
        {
            DateTime date = DateTime.Today;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlStatement = "INSERT INTO dbo.Meetings (Date, MeetingRequester, PhoneNumber, RequestContent) VALUES (@Date, @MeetingRequester, @PhoneNumber, @RequestContent)";

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@Date", System.Data.SqlDbType.Date).Value = date;
                command.Parameters.Add("@MeetingRequester", System.Data.SqlDbType.NChar, 40).Value = meeting.MeetingRequester;
                command.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.NChar, 8).Value = meeting.PhoneNumber;
                command.Parameters.Add("@RequestContent", System.Data.SqlDbType.NVarChar, int.MaxValue).Value = meeting.RequestContent;

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


    }
}

