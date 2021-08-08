using Microsoft.Data.SqlClient;
using SparEnergi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SparEnergi.Services
{
    public class UserService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SparEnergiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
           
        public UserModel FindUserByUsernameAndPassword(string username, string password)
        {
            UserModel user = new UserModel();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlStatement = "SELECT * FROM [dbo].[Users] WHERE username = @username AND password = @password";

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.NChar).Value = username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NChar).Value = password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.Username = reader["Username"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.EmailAddress = reader["EmailAddress"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.StreetName = reader["StreetName"].ToString();
                        user.PostCode = Convert.ToInt32(reader["PostCode"]);
                    };
                    if(Regex.Replace(user.Password, @"\s+", "") == password)
                    {
                        return user;
                    }                   
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return null;

            }      

        }

        public UserModel GetUserById(int id)
        {
            UserModel user = new UserModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlStatement = "SELECT * FROM [dbo].[Users] WHERE id = @id";

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["id"]);
                        user.Username = reader["Username"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.EmailAddress = reader["EmailAddress"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.StreetName = reader["StreetName"].ToString();
                        user.PostCode = Convert.ToInt32(reader["PostCode"]);
                    };
                    return user;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return null;
                }


            }
        }

        public int RegisterNewUser(UserModel user)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlStatement = "INSERT INTO [dbo].[Users] (username, password, emailaddress, firstname, lastname, streetname, postcode) VALUES (@username, @password, @emailaddress, @firstname, @lastname, @streetname, @postcode)";

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.NChar, 50).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NChar, 50).Value = user.Password;
                command.Parameters.Add("@emailaddress", System.Data.SqlDbType.NChar, 50).Value = user.EmailAddress;
                command.Parameters.Add("@firstname", System.Data.SqlDbType.NChar, 50).Value = user.FirstName;
                command.Parameters.Add("@lastname", System.Data.SqlDbType.NChar, 50).Value = user.LastName;
                command.Parameters.Add("@streetname", System.Data.SqlDbType.NChar, 50).Value = user.StreetName;
                command.Parameters.Add("@postcode", System.Data.SqlDbType.Int).Value = user.PostCode;

                try
                {
                    connection.Open();
                    return (command.ExecuteNonQuery());
                                    } 
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return 0;
                }
            }
        }

    }
}
