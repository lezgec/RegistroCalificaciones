using Microsoft.Data.Sqlite;
using RegistroCalificaciones.Models;
using System.Diagnostics;

namespace RegistroCalificaciones.Helpers
{
    public static class GradeRepository
    {
        public static Dictionary<string, Grade> GetGradesByStudentAndSubject(int studentId, int subjectId)
        {
            var result = new Dictionary<string, Grade>();

            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Type, Value, Date FROM Grades WHERE StudentId = $studentId AND SubjectId = $subjectId";
            command.Parameters.AddWithValue("$studentId", studentId);
            command.Parameters.AddWithValue("$subjectId", subjectId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var grade = new Grade
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    Type = reader.GetString(0),
                    Value = reader.GetDouble(1),
                    Date = reader.IsDBNull(2) ? null : reader.GetString(2)
                };

                result[grade.Type] = grade;
            }

            return result;
        }

        public static void Upsert(int studentId, int subjectId, string type, double value)
        {
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Grades (StudentId, SubjectId, Type, Value, Date)
                VALUES ($studentId, $subjectId, $type, $value, $date)
                ON CONFLICT(StudentId, SubjectId, Type) DO UPDATE SET 
                    Value = excluded.Value,
                    Date = excluded.Date;";

            command.Parameters.AddWithValue("$studentId", studentId);
            command.Parameters.AddWithValue("$subjectId", subjectId);
            command.Parameters.AddWithValue("$type", type);
            command.Parameters.AddWithValue("$value", value);
            command.Parameters.AddWithValue("$date", DateTime.Now.ToString("yyyy-MM-dd"));

            command.ExecuteNonQuery();
        }
    }
}
