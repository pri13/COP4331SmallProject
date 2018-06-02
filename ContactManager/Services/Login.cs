
using System;
using System.Web;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace ContactManager.Services
{

    public struct LoginRequest
    {
        public string login, password;
    }

    public struct LoginResponse
    {
        public int id;
        public string firstName, lastName;
        public string error;
    }
    public class Login
    { 

        public LoginResponse GetAccess(string username, string password)
        {

            LoginRequest req;
            LoginResponse res = new LoginResponse();
            res.error = String.Empty;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString);
            try
            {
                connection.Open();

                string sql = String.Format("select ID,FirstName,LastName from Users where Login='{0}' and Password='{1}'", username, password);
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    res.id = Convert.ToInt32(reader["ID"]);
                    res.firstName = Convert.ToString(reader["FirstName"]);
                    res.lastName = Convert.ToString(reader["LastName"]);
                    
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                res.error = ex.Message.ToString();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return res;
        }
        

       

    }
}
