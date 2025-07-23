using DocumentFormat.OpenXml.InkML;
using Microsoft.Data.Sqlite;
using RegistroCalificaciones.Models;

namespace RegistroCalificaciones.Helpers
{
    public static class CourseRepository
    {
        public static List<Course> GetAll()
        {
            var courses = new List<Course>();
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name FROM Courses WHERE Activo = 1 ORDER BY Name";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                courses.Add(new Course
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return courses;
        }

        public static void Add(string name)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Courses (Name) VALUES ($name)";
            command.Parameters.AddWithValue("$name", name);
            command.ExecuteNonQuery();
        }
        public static void Delete(int courseId)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            // Marcar curso como inactivo
            var updateCourse = connection.CreateCommand();
            updateCourse.CommandText = "UPDATE Courses SET Activo = 0 WHERE Id = $id";
            updateCourse.Parameters.AddWithValue("$id", courseId);
            updateCourse.ExecuteNonQuery();

            // Marcar todos los estudiantes de ese curso como inactivos
            var updateStudents = connection.CreateCommand();
            updateStudents.CommandText = "UPDATE Students SET Activo = 0 WHERE CourseId = $id";
            updateStudents.Parameters.AddWithValue("$id", courseId);
            updateStudents.ExecuteNonQuery();
        }

        public static Course? GetByName(string name)
        {
            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, Name, Activo FROM Courses WHERE Name = @name AND Activo = 1";
                    command.Parameters.AddWithValue("@name", name);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Course
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Activo = reader.GetInt32(2) == 1
                            };
                        }
                    }
                }
            }

            return null;
        }



    }
}
