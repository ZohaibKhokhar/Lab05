using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Lab05.Models
{
    public class UserRepository
    {
        public string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=labs;Integrated Security=True;";

        public void AddUser(User user)
        {
            string query = "INSERT INTO Users (Name, Email, Password, CreatedDate) VALUES (@name, @email, @pass, @date)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    cmd.Parameters.AddWithValue("@date", user.CreatedDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM Users";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["Password "]),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public void DeleteUser(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            string query = "UPDATE Users SET Name = @name, Email = @email, Password = @pass, CreatedDate = @date WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@pass", user.Password);
                    cmd.Parameters.AddWithValue("@date", user.CreatedDate);
                    cmd.Parameters.AddWithValue("@id", user.Id); // Add this line to specify the Id
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User GetUserById(int id)
        {
            User user = null;
            string query = "SELECT * FROM Users WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id); // Add this line to specify the Id
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["Password "]),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        };
                    }
                }
            }
            return user;
        }
    }
}
