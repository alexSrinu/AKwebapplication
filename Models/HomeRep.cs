using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AKwebapplication.Models
{
    public class HomeRep
    {
        private readonly string _connectionString;

        // Constructor to initialize the connection string
        public HomeRep()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public void InsertPerson(Home person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("InsertPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Mobile", person.Mobile);
                    command.Parameters.AddWithValue("@JoinDate", person.JoinDate);
                    command.Parameters.AddWithValue("@FeePaidDate", (object)person.FeePaidDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DueDate", (object)person.DueDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Occupation", person.Occupation);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Address", person.Address);
                    command.Parameters.AddWithValue("@Sharing", person.Sharing);
                    command.Parameters.AddWithValue("@ImagePath", person.FileName ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void UpdatePerson(Home person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", person.Id);
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Mobile", person.Mobile);
                    command.Parameters.AddWithValue("@JoinDate", person.JoinDate);
                    command.Parameters.AddWithValue("@FeePaidDate", (object)person.FeePaidDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DueDate", (object)person.DueDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Occupation", person.Occupation);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Address", person.Address);
                    command.Parameters.AddWithValue("@Sharing", person.Sharing);
                    command.Parameters.AddWithValue("@ImagePath", person.FileName ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeletePerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public Home GetPersonById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetPersonById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Home
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                JoinDate = (DateTime)reader["JoinDate"],
                                FeePaidDate = reader["FeePaidDate"] as DateTime?,
                                DueDate = reader["DueDate"] as DateTime?,
                                Occupation = reader["Occupation"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Address = reader["Address"].ToString(),
                                Sharing = reader["Sharing"].ToString(),
                                IsActive = (bool)reader["IsActive"],
                                FileName = reader["ImagePath"]?.ToString()
                            };
                        }
                    }
                }
            }
            return null; // Return null if not found
        }

        public IEnumerable<Home> GetAllActivePersons()
        {
            var persons = new List<Home>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetAllActivePersons", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            persons.Add(new Home
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                JoinDate = (DateTime)reader["JoinDate"],
                                FeePaidDate = reader["FeePaidDate"] as DateTime?,
                                DueDate = reader["DueDate"] as DateTime?,
                                Occupation = reader["Occupation"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Address = reader["Address"].ToString(),
                                Sharing = reader["Sharing"].ToString(),
                                IsActive = (bool)reader["IsActive"],
                                FileName = reader["ImagePath"]?.ToString()
                            });
                        }
                    }
                }
            }

            return persons;
        }
    }
}
