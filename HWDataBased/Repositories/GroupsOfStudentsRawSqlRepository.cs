using System.Data;
using System.Data.SqlClient;
using HWDataBased.Models;

namespace HWDataBased.Repositories
{
    public class GroupsOfStudentsRawSqlRepository : IGroupsOfStudents
    {
        private readonly string _connectionString;
        public GroupsOfStudentsRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddStudentInGroup(GroupsOfStudents groupsOfStudents)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [GroupsOfStudents]
                        values
                            (@studentId, @groupId)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@studentId", SqlDbType.NVarChar).Value = groupsOfStudents.StudentId;
                    command.Parameters.Add("@groupId", SqlDbType.NVarChar).Value = groupsOfStudents.GroupId;

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<GroupsOfStudents> GetStudentAndGroupsById()
        {
            var result = new List<GroupsOfStudents>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"select [StudentId], [GroupsId]
                        from [GroupsOfStudents]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GroupsOfStudents
                            {
                                GroupId = Convert.ToInt32(reader["StudentId"]),
                                StudentId = Convert.ToInt32(reader["GroupsId"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<GroupsOfStudents> GetAllStudentByGroupId()
        {
            var result = new List<GroupsOfStudents>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select [StudentId], [GroupsId] 
                                        from [GroupsOfStudents]
                                        where[GroupsId] = @groupsId";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GroupsOfStudents
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                            });
                        }
                    }
                }
            }
            return result;
        }

    }
}