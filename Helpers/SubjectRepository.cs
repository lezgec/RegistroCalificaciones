using Microsoft.Data.Sqlite;
using RegistroCalificaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroCalificaciones.Helpers
{
    public static class SubjectRepository
    {
        public static void CrearTablaSiNoExiste()
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Subjects (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Teacher TEXT,
                CourseId INTEGER NOT NULL,
                Activo INTEGER NOT NULL DEFAULT 1,
                FOREIGN KEY (CourseId) REFERENCES Courses(Id)
            );";
            command.ExecuteNonQuery();
        }

        public static List<Subject> GetByCourseId(int courseId)
        {
            var list = new List<Subject>();

            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, Teacher, Activo FROM Subjects WHERE CourseId = @courseId";
            command.Parameters.AddWithValue("@courseId", courseId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Subject
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Teacher = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    CourseId = courseId,
                    Activo = reader.GetInt32(3) == 1
                });
            }

            return list;
        }

        public static void InsertOrUpdate(Subject subject)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();

            if (subject.Id == 0)
            {
                command.CommandText = @"
                INSERT INTO Subjects (Name, Teacher, CourseId, Activo)
                VALUES (@name, @teacher, @courseId, @activo)";
            }
            else
            {
                command.CommandText = @"
                UPDATE Subjects
                SET Name = @name, Teacher = @teacher, Activo = @activo
                WHERE Id = @id";
                command.Parameters.AddWithValue("@id", subject.Id);
            }

            command.Parameters.AddWithValue("@name", subject.Name);
            command.Parameters.AddWithValue("@teacher", subject.Teacher ?? "");
            command.Parameters.AddWithValue("@courseId", subject.CourseId);
            command.Parameters.AddWithValue("@activo", subject.Activo ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public static void LogicalDelete(int subjectId)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Subjects SET Activo = 0 WHERE Id = @id";
            command.Parameters.AddWithValue("@id", subjectId);
            command.ExecuteNonQuery();
        }

        public static void Add(string name, string docente, int courseId)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
        INSERT INTO Subjects (Name, Docente, CourseId, Activo)
        VALUES ($name, $docente, $courseId, 1);
    ";

            command.Parameters.AddWithValue("$name", name);
            command.Parameters.AddWithValue("$docente", docente);
            command.Parameters.AddWithValue("$courseId", courseId);

            command.ExecuteNonQuery();
        }

    }
}
