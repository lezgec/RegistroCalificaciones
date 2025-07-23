using RegistroCalificaciones.Helpers;

namespace RegistroCalificaciones
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Crea la base si no existe
            DatabaseHelper.InitializeDatabase();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
