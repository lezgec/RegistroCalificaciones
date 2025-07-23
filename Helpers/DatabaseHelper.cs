using Microsoft.Data.Sqlite;

namespace RegistroCalificaciones.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly string FolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RegistroCalificaciones");

        private static readonly string DbPath = Path.Combine(FolderPath, "calificaciones.db");

        public static string ConnectionString => $"Data Source={DbPath}";

        public static void InitializeDatabase()
        {
            // Crear la carpeta si no existe
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            if (File.Exists(DbPath)) return;

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE Courses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Activo INTEGER DEFAULT 1
                );

                CREATE TABLE Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    CourseId INTEGER,
                    Activo INTEGER DEFAULT 1,
                    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
                );

                CREATE TABLE IF NOT EXISTS Grades (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentId INTEGER NOT NULL,
                    SubjectId INTEGER NOT NULL,
                    Type TEXT NOT NULL,
                    Value REAL,
                    Date TEXT,
                    FOREIGN KEY (StudentId) REFERENCES Students(Id),
                    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id)
                );

                CREATE UNIQUE INDEX IF NOT EXISTS idx_grade_unique
                    ON Grades (StudentId, SubjectId, Type);

                CREATE TABLE IF NOT EXISTS Subjects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Teacher TEXT,
                    CourseId INTEGER NOT NULL,
                    Activo INTEGER NOT NULL DEFAULT 1,
                    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
                );

                CREATE TABLE IF NOT EXISTS NoteTypes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    Weight REAL DEFAULT 1, -- ponderación futura
                    Activo INTEGER NOT NULL DEFAULT 1,
                    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id)
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}
