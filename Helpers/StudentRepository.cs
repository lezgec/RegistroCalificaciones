using Microsoft.Data.Sqlite;
using RegistroCalificaciones.Models;

namespace RegistroCalificaciones.Helpers
{
    public static class StudentRepository
    {
        public static List<Student> GetByCourseId(int courseId)
        {
            var students = new List<Student>();

            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name FROM Students WHERE CourseId = $courseId AND Activo = 1 ORDER BY Name";
            command.Parameters.AddWithValue("$courseId", courseId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    CourseId = courseId,
                    Activo = true
                });
            }

            return students;
        }

        public static void Add(string name, int courseId)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Students (Name, CourseId) VALUES ($name, $courseId)";
            command.Parameters.AddWithValue("$name", name);
            command.Parameters.AddWithValue("$courseId", courseId);
            command.ExecuteNonQuery();
        }
        public static void Delete(int studentId)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Students SET Activo = 0 WHERE Id = $id";
            command.Parameters.AddWithValue("$id", studentId);
            command.ExecuteNonQuery();
        }

        public static bool Exists(string name, int courseId)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                SELECT COUNT(1)
                FROM Students
                WHERE Name = @name AND CourseId = @courseId AND Activo = 1;
            ";
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@courseId", courseId);

                    var result = Convert.ToInt32(command.ExecuteScalar());
                    return result > 0;
                }
            }
        }


    }
}
