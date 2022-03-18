using HWDataBased.Models;
using System;
using System.Data;
using System.Data.SqlClient;


namespace HWDataBased.Repositories
{
    public class StudentRawSqlRepository : IStudentRepository
    {
        private readonly string _connectionString;
        public StudentRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddStudent(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [Students]
                            ([Name], [Age])
                        values
                            (@name, @age)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = student.Name;
                    command.Parameters.Add("@age", SqlDbType.Int).Value = student.Age;

                    student.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public Student GetStudentById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"select [Id], [Name], [Age]
                        from [Students]
                        where [Id] = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Age = Convert.ToInt32(reader["Age"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            var result = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [Id], [Name], [Age] from [Students]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Age = Convert.ToInt32(reader["Age"])
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}
