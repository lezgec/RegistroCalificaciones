namespace RegistroCalificaciones.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Type { get; set; } = string.Empty;  // "Carpeta", "Leccion1", etc.
        public double Value { get; set; }
        public string? Date { get; set; }  // Opcional para más adelante
    }
}
