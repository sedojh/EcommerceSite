using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class Register : Data
    {
        //retrieve hashed password
        public static void RegisterPassword(string username, string hashedpass)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO Customers VALUES(@username,@hash)", conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@hash", hashedpass);
                command.ExecuteNonQuery();
            }
            return;
        }
    }
}