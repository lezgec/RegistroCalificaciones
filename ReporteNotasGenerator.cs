using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RegistroCalificaciones.Models;

namespace RegistroCalificaciones.Reportes
{
    public class ReporteNotasGenerator
    {
        private readonly Course _curso;
        private readonly Subject _materia;
        private readonly List<Student> _estudiantes;
        private readonly Func<int, Dictionary<string, Grade>> _obtenerNotas;

        public byte[] CargarImagenCabecera()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "header.png");
            return File.ReadAllBytes(path);
        }
        public ReporteNotasGenerator(Course curso, Subject materia, List<Student> estudiantes, Func<int, Dictionary<string, Grade>> obtenerNotas)
        {
            _curso = curso;
            _materia = materia;
            _estudiantes = estudiantes;
            _obtenerNotas = obtenerNotas;
        }

        public void GenerarPDF(string rutaArchivo)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Height(80).Image(CargarImagenCabecera(), ImageScaling.FitWidth);

                page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Curso: {_curso.Name}");
                        col.Item().Text($"Materia: {_materia.Name}");
                        col.Item().Text($"Docente: {_materia.Teacher}");
                        col.Item().Text($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy}");
                        col.Item().Text($"Cantidad de estudiantes: {_estudiantes.Count}");

                        col.Item().PaddingTop(15).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2); // Nombre
                                columns.ConstantColumn(60); // Carpeta
                                columns.ConstantColumn(60); // Lección 1
                                columns.ConstantColumn(60); // Lección 2
                                columns.ConstantColumn(60); // Examen
                                columns.ConstantColumn(60); // Promedio
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Estudiante");
                                header.Cell().Element(CellStyle).Text("Carpeta");
                                header.Cell().Element(CellStyle).Text("Lec. 1");
                                header.Cell().Element(CellStyle).Text("Lec. 2");
                                header.Cell().Element(CellStyle).Text("Examen");
                                header.Cell().Element(CellStyle).Text("Prom.");

                                static IContainer CellStyle(IContainer container) =>
                                    container.DefaultTextStyle(x => x.SemiBold()).Background(Colors.Grey.Lighten2).Padding(5);
                            });

                            foreach (var estudiante in _estudiantes)
                            {
                                var notas = _obtenerNotas(estudiante.Id);

                                double carpeta = notas.TryGetValue("Carpeta", out var c) ? c.Value : 0;
                                double l1 = notas.TryGetValue("Leccion1", out var l) ? l.Value : 0;
                                double l2 = notas.TryGetValue("Leccion2", out var l2v) ? l2v.Value : 0;
                                double ex = notas.TryGetValue("Examen", out var e) ? e.Value : 0;
                                double prom = Math.Round((carpeta + l1 + l2 + ex) / 4, 2);

                                table.Cell().Element(e => e.Padding(5)).Text(estudiante.Name);
                                table.Cell().Element(e => e.Padding(5).AlignCenter()).Text(carpeta.ToString("0.00"));
                                table.Cell().Element(e => e.Padding(5).AlignCenter()).Text(l1.ToString("0.00"));
                                table.Cell().Element(e => e.Padding(5).AlignCenter()).Text(l2.ToString("0.00"));
                                table.Cell().Element(e => e.Padding(5).AlignCenter()).Text(ex.ToString("0.00"));
                                table.Cell().Element(e => e.Padding(5).AlignCenter()).Text(prom.ToString("0.00"));
                            }
                        });
                    });

                    page.Footer().AlignRight().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf(rutaArchivo);
        }
    }
}
