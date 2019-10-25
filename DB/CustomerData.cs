using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class CustomerData : Data
    {
        //getting all customers data
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * from Customers";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer();
                    {
                        customer.Id = (string)reader["Id"];
                        customer.Name = (string)reader["Name"];
                        customer.Username = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.Email = (string)reader["Email"];
                        customer.Address = (string)reader["Address"];
                        customer.Postalcode = (int)reader["Postalcode"];
                        customer.Phone = (int)reader["Phone"];
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }

        //getting customer details from username
        public static Customer GetCustomerDetails(string username)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select * from Customers WHERE Username = @text", conn);
                command.Parameters.AddWithValue("@text", username);
                SqlDataReader reader = command.ExecuteReader();
                Customer customer = new Customer();

                if (reader.Read())
                {
                    {
                        customer.Id = (string)reader["Id"];
                        customer.Name = (string)reader["Name"];
                        customer.Username = (string)reader["Username"];
                        customer.Password = (string)reader["Password"];
                        customer.Email = (string)reader["Email"];
                        customer.Address = (string)reader["Address"];
                        customer.Postalcode = (int)reader["Postalcode"];
                        customer.Phone = (int)reader["Phone"];
                        return customer;
                    };
                }
                else
                {
                    customer.Id = "FALSE";
                    return customer;
                }
            }
        }

        //will return true if email address is in database
        public static bool CheckCustomerEmail(string email)
        {
            bool found = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select * from Customers WHERE Email = @text", conn);
                command.Parameters.AddWithValue("@text", email);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    found = true;
                }
            }
            return found;
        }

        //to update customer database for new registration
        public static void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO Customers VALUES(@id,@name,@username,@password,@email,@address,'" + customer.Postalcode + "','" + customer.Phone + "')", conn);
                command.Parameters.AddWithValue("@id", customer.Id);
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@username", customer.Username);
                command.Parameters.AddWithValue("@password", customer.Password);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.ExecuteNonQuery();
            }
            return;
        }

        //check if username exists in database
        public static bool CheckUsername(string username)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select * from Customers WHERE Username = @text", conn);
                command.Parameters.AddWithValue("@text", username);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}