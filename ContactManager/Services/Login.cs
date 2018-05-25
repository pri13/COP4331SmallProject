
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

        public void GetAccess()
        {

            LoginRequest req;
            LoginResponse res = new LoginResponse();
            res.error = String.Empty;

            // 1. Deserialize the incoming Json.
            try
            {
                req = GetRequestInfo();
            }
            catch (Exception ex)
            {
                res.error = ex.Message.ToString();

                // Return the results as Json.
             //   SendResultInfoAsJson(res);

                return;
            }

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            try
            {
                connection.Open();

                string sql = String.Format("select ID,FirstName,LastName from Users where Login='{0}' and Password='{1}'", req.login, req.password);
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

            // Return the results as Json.
         //   SendResultInfoAsJson(res);
        }

        LoginRequest GetRequestInfo()
        {
            // Get the Json from the POST.
            string strJson = String.Empty;
            HttpContext context = HttpContext.Current;
            context.Request.InputStream.Position = 0;
            using (StreamReader inputStream = new StreamReader(context.Request.InputStream))
            {
                strJson = inputStream.ReadToEnd();
            }

            // Deserialize the Json.
            LoginRequest req = JsonConvert.DeserializeObject<LoginRequest>(strJson);

            return (req);
        }

        //void SendResultInfoAsJson(LoginResponse res)
        //{
        //    string strJson = JsonConvert.SerializeObject(res);
        //    Response.ContentType = "application/json; charset=utf-8";
        //    Response.Write(strJson);
        //    Response.End();
        //}

    }
}
