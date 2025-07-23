using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using ExcelDataReader;
using QuestPDF.Infrastructure;
using RegistroCalificaciones.Helpers;
using RegistroCalificaciones.Models;
using RegistroCalificaciones.Reportes;
using System.Data;
using System.Text;
using Color = System.Drawing.Color;


namespace RegistroCalificaciones
{
    public partial class Form1 : Form
    {
        private List<Course> cursos = new();
        private Course? cursoSeleccionado;
        private List<Student> estudiantesCursoActual = new();

        public Form1()
        {
            
            QuestPDF.Settings.License = LicenseType.Community;
            InitializeComponent();
            this.Text = "Sistema de Registro de Calificaciones";
            this.Icon = new Icon("Resources/logo.ico");
            CargarCursos();
        }

        private void CargarCursos()
        {
            cursos = CourseRepository.GetAll();
            comboCursos.Items.Clear();
            foreach (var curso in cursos)
            {
                comboCursos.Items.Add(curso.Name);
            }

            if (comboCursos.Items.Count > 0)
                comboCursos.SelectedIndex = 0;

        }

        private void comboCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCursos.SelectedIndex < 0 || cursos.Count == 0)
                return;

            cursoSeleccionado = cursos[comboCursos.SelectedIndex];
            CargarMateriasDelCurso();
        }

        private void btnAgregarCurso_Click(object sender, EventArgs e)
        {
            string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre del nuevo curso:", "Agregar curso");
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                CourseRepository.Add(nuevoNombre.Trim());
                CargarCursos();
            }
        }

        private void CargarEstudiantes()
        {
            if (cursoSeleccionado == null) return;

            estudiantesCursoActual = StudentRepository.GetByCourseId(cursoSeleccionado.Id)
                .Where(e => e.Activo).ToList();


            gridEstudiantes.Rows.Clear();
            gridEstudiantes.Columns.Clear();

            gridEstudiantes.Columns.Add("Id", "ID");
            gridEstudiantes.Columns["Id"].Visible = false;

            gridEstudiantes.Columns.Add("Numero", "N°");
            gridEstudiantes.Columns["Numero"].ReadOnly = true;
            gridEstudiantes.Columns["Numero"].Width = 20; // ancho fijo opcional

            gridEstudiantes.Columns.Add("Nombre", "Nombre del Estudiante");
            gridEstudiantes.Columns["Nombre"].ReadOnly = true;
            gridEstudiantes.Columns["Nombre"].Width = 300;

            gridEstudiantes.Columns.Add("Carpeta", "Carpeta");
            gridEstudiantes.Columns.Add("FechaCarpeta", "Fecha Carpeta");

            gridEstudiantes.Columns.Add("Leccion1", "Lección 1");
            gridEstudiantes.Columns.Add("FechaLeccion1", "Fecha L1");

            gridEstudiantes.Columns.Add("Leccion2", "Lección 2");
            gridEstudiantes.Columns.Add("FechaLeccion2", "Fecha L2");

            gridEstudiantes.Columns.Add("Examen", "Examen");
            gridEstudiantes.Columns.Add("FechaExamen", "Fecha Examen");

            gridEstudiantes.Columns.Add("Total", "Total");
            gridEstudiantes.Columns["Total"].ReadOnly = true;

            gridEstudiantes.Columns.Add("Promedio", "Promedio");
            gridEstudiantes.Columns["Promedio"].ReadOnly = true;

            int contador = 1;
            foreach (var est in estudiantesCursoActual)
            {
                var notas = materiaSeleccionada != null
                    ? GradeRepository.GetGradesByStudentAndSubject(est.Id, materiaSeleccionada.Id)
                    : new Dictionary<string, Grade>();

                double carpeta = notas.TryGetValue("Carpeta", out var c) ? c.Value : 0;
                double l1 = notas.TryGetValue("Leccion1", out var l) ? l.Value : 0;
                double l2 = notas.TryGetValue("Leccion2", out var l2v) ? l2v.Value : 0;
                double ex = notas.TryGetValue("Examen", out var e) ? e.Value : 0;

                double total = carpeta + l1 + l2 + ex;
                double promedio = total / 4;

                string fechaC = c?.Date ?? "";
                string fechaL1 = l?.Date ?? "";
                string fechaL2 = l2v?.Date ?? "";
                string fechaEx = e?.Date ?? "";

                gridEstudiantes.Rows.Add(
                    est.Id, contador++, est.Name,
                    carpeta, fechaC,
                    l1, fechaL1,
                    l2, fechaL2,
                    ex, fechaEx,
                    total.ToString("0.00"), promedio.ToString("0.00"));
            }
        }




        private void ActualizarGridConFiltro(string filtro)
        {
            var estudiantesFiltrados = estudiantesCursoActual
                .Where(e => string.IsNullOrWhiteSpace(filtro) || e.Name.ToLower().Contains(filtro.ToLower()))
                .ToList();

            gridEstudiantes.Rows.Clear();
            int numero = 1;
            foreach (var est in estudiantesFiltrados)
            {
                var notas = materiaSeleccionada != null
                    ? GradeRepository.GetGradesByStudentAndSubject(est.Id, materiaSeleccionada.Id)
                    : new Dictionary<string, Grade>();

                double carpeta = notas.TryGetValue("Carpeta", out var c) ? c.Value : 0;
                double l1 = notas.TryGetValue("Leccion1", out var l) ? l.Value : 0;
                double l2 = notas.TryGetValue("Leccion2", out var l2v) ? l2v.Value : 0;
                double ex = notas.TryGetValue("Examen", out var e) ? e.Value : 0;

                double total = carpeta + l1 + l2 + ex;
                double promedio = total / 4;

                string fechaC = c?.Date ?? "";
                string fechaL1 = l?.Date ?? "";
                string fechaL2 = l2v?.Date ?? "";
                string fechaEx = e?.Date ?? "";

                gridEstudiantes.Rows.Add(est.Id, numero++, est.Name,
                    carpeta, fechaC,
                    l1, fechaL1,
                    l2, fechaL2,
                    ex, fechaEx,
                    total.ToString("0.00"), promedio.ToString("0.00"));
            }
        }


        private void btnAgregarEstudiante_Click(object sender, EventArgs e)
        {
            if (cursoSeleccionado == null) return;

            string nombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre del estudiante:", "Agregar estudiante");
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                StudentRepository.Add(nombre.Trim(), cursoSeleccionado.Id);
                CargarEstudiantes();
            }
        }

        private void gridEstudiantes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || materiaSeleccionada == null) return;

            var row = gridEstudiantes.Rows[e.RowIndex];
            var columnName = gridEstudiantes.Columns[e.ColumnIndex].Name;

            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int studentId)) return;

            // Validar solo si es una columna de nota
            if (columnName is "Carpeta" or "Leccion1" or "Leccion2" or "Examen")
            {
                if (!double.TryParse(row.Cells[columnName].Value?.ToString(), out double valor) || valor < 0 || valor > 10)
                {
                    MessageBox.Show("La nota debe estar entre 0.00 y 10.00", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Restaurar a 0 si es inválido
                    row.Cells[columnName].Value = 0;
                    row.Cells[columnName].Style.BackColor = Color.LightCoral;
                    return;
                }

                string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

                // Guardar nota con SubjectId
                GradeRepository.Upsert(studentId, materiaSeleccionada.Id, columnName, valor);

                // Actualizar celda de fecha asociada
                string nombreColumnaFecha = columnName switch
                {
                    "Carpeta" => "FechaCarpeta",
                    "Leccion1" => "FechaLeccion1",
                    "Leccion2" => "FechaLeccion2",
                    "Examen" => "FechaExamen",
                    _ => null
                };

                if (!string.IsNullOrEmpty(nombreColumnaFecha))
                {
                    row.Cells[nombreColumnaFecha].Value = fechaActual;
                }

                // Recalcular Total y Promedio
                double carpeta = Convert.ToDouble(row.Cells["Carpeta"].Value ?? 0);
                double l1 = Convert.ToDouble(row.Cells["Leccion1"].Value ?? 0);
                double l2 = Convert.ToDouble(row.Cells["Leccion2"].Value ?? 0);
                double ex = Convert.ToDouble(row.Cells["Examen"].Value ?? 0);

                double total = carpeta + l1 + l2 + ex;
                double promedio = total / 4;

                row.Cells["Total"].Value = total.ToString("0.00");
                row.Cells["Promedio"].Value = promedio.ToString("0.00");

                // Limpiar color si estaba en error
                row.Cells[columnName].Style.BackColor = Color.White;
            }
        }




        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (gridEstudiantes.Rows.Count == 0 || materiaSeleccionada == null)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Notas");

            // 🔹 Columnas a excluir
            var columnasExcluir = new[] { "Id", "FechaCarpeta", "FechaLeccion1", "FechaLeccion2", "FechaExamen" };

            // 🔹 Insertar imagen de encabezado
            string imagePath = "Resources/header.png";
            if (File.Exists(imagePath))
            {
                string ultimaColumna = XLHelper.GetColumnLetterFromNumber(gridEstudiantes.Columns.Count - columnasExcluir.Length);
                var image = worksheet.AddPicture(imagePath)
                                     .MoveTo(worksheet.Cell("A1"))
                                     .WithPlacement(XLPicturePlacement.Move)
                                     .WithSize(700, 80);
                worksheet.Range($"A1:{ultimaColumna}4").Merge();
            }

            // 🔹 Información general
            worksheet.Cell("A5").Value = "Curso:";
            worksheet.Cell("B5").Value = cursoSeleccionado?.Name ?? "N/A";

            worksheet.Cell("A6").Value = "Materia:";
            worksheet.Cell("B6").Value = materiaSeleccionada.Name;

            worksheet.Cell("A7").Value = "Docente:";
            worksheet.Cell("B7").Value = materiaSeleccionada.Teacher;

            // 🔹 Encabezados (fila 9)
            int colExcel = 1;
            for (int col = 0; col < gridEstudiantes.Columns.Count; col++)
            {
                var nombreCol = gridEstudiantes.Columns[col].Name;
                if (columnasExcluir.Contains(nombreCol)) continue;

                worksheet.Cell(9, colExcel).Value = gridEstudiantes.Columns[col].HeaderText;
                worksheet.Cell(9, colExcel).Style.Font.Bold = true;
                colExcel++;
            }

            // 🔹 Filas de datos
            for (int row = 0; row < gridEstudiantes.Rows.Count; row++)
            {
                int colExport = 1;
                for (int col = 0; col < gridEstudiantes.Columns.Count; col++)
                {
                    var nombreCol = gridEstudiantes.Columns[col].Name;
                    if (columnasExcluir.Contains(nombreCol)) continue;

                    worksheet.Cell(row + 10, colExport).Value = gridEstudiantes.Rows[row].Cells[col].Value?.ToString();
                    colExport++;
                }
            }

            worksheet.Columns().AdjustToContents();

            using var saveFileDialog = new SaveFileDialog
            {
                Title = "Guardar archivo Excel",
                Filter = "Archivo Excel (*.xlsx)|*.xlsx",
                FileName = $"Notas_{cursoSeleccionado?.Name}_{materiaSeleccionada?.Name}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show($"✅ Exportado correctamente:\n{saveFileDialog.FileName}", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al exportar: " + ex.Message);
                }
            }
        }





        private void btnImportarEstudiantes_Click(object sender, EventArgs e)
        {
            if (cursoSeleccionado == null)
            {
                MessageBox.Show("Primero selecciona un curso.");
                return;
            }

            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos Excel (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Selecciona un archivo de Excel"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    using var reader = ExcelReaderFactory.CreateReader(stream);

                    var result = reader.AsDataSet();
                    var table = result.Tables[0];

                    int countAgregados = 0;

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var nombre = table.Rows[i][0]?.ToString()?.Trim();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            StudentRepository.Add(nombre, cursoSeleccionado.Id);
                            countAgregados++;
                        }
                    }

                    MessageBox.Show($"Se importaron {countAgregados} estudiantes.");
                    CargarEstudiantes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al leer el archivo: " + ex.Message);
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ActualizarGridConFiltro(txtBuscar.Text);
        }

        private void btnEliminarEstudiante_Click(object sender, EventArgs e)
        {
            if (gridEstudiantes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una fila para eliminar.");
                return;
            }

            var row = gridEstudiantes.SelectedRows[0];

            if (!int.TryParse(row.Cells["Id"].Value?.ToString(), out int studentId))
            {
                MessageBox.Show("No se pudo obtener el ID del estudiante.");
                return;
            }

            var nombre = row.Cells["Nombre"].Value?.ToString() ?? "";

            var confirm = MessageBox.Show($"¿Deseas eliminar al estudiante:\n\n{nombre}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                StudentRepository.Delete(studentId);
                CargarEstudiantes();
            }
        }

        private void btnEliminarCurso_Click(object sender, EventArgs e)
        {
            if (cursoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un curso primero.");
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Estás seguro de que deseas eliminar el curso '{cursoSeleccionado.Name}'?\n\n" +
                "Esto eliminará todos sus estudiantes y calificaciones.",
                "Eliminar curso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                CourseRepository.Delete(cursoSeleccionado.Id);
                cursoSeleccionado = null;
                CargarCursos(); // Recarga el ComboBox
                gridEstudiantes.Rows.Clear();
                gridEstudiantes.Columns.Clear();
            }
        }

        private void btnImportarNotas_Click(object sender, EventArgs e)
        {
            if (cursoSeleccionado == null)
            {
                MessageBox.Show("Primero selecciona un curso.");
                return;
            }

            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos Excel (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Selecciona archivo de Excel con notas"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();
                var table = result.Tables[0];

                var estudiantesExistentes = StudentRepository.GetByCourseId(cursoSeleccionado.Id);
                var nombresNoEncontrados = new List<string>();
                int notasAsignadas = 0;

                bool crearEstudiantesFaltantes = false;

                if (MessageBox.Show("¿Deseas crear automáticamente los estudiantes que no se encuentren?", "Opcional", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    crearEstudiantesFaltantes = true;
                }

                for (int i = 1; i < table.Rows.Count; i++) // omitir encabezado
                {
                    string? nombre = table.Rows[i][0]?.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(nombre)) continue;

                    var estudiante = estudiantesExistentes.FirstOrDefault(e => e.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase));

                    if (estudiante == null)
                    {
                        if (crearEstudiantesFaltantes)
                        {
                            StudentRepository.Add(nombre, cursoSeleccionado.Id);
                            estudiante = StudentRepository.GetByCourseId(cursoSeleccionado.Id)
                                .FirstOrDefault(e => e.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                            if (estudiante != null)
                                estudiantesExistentes.Add(estudiante);
                        }
                        else
                        {
                            nombresNoEncontrados.Add(nombre);
                            continue;
                        }
                    }

                    string[] tipos = { "Carpeta", "Leccion1", "Leccion2", "Examen" };

                    for (int j = 0; j < tipos.Length; j++)
                    {
                        if (double.TryParse(table.Rows[i][j + 1]?.ToString(), out double valor) && valor >= 0 && valor <= 10)
                        {
                            GradeRepository.Upsert(estudiante.Id, materiaSeleccionada.Id, tipos[j], valor);

                        }
                    }

                    notasAsignadas++;
                }

                CargarEstudiantes();

                string resumen = $"✅ Notas asignadas para {notasAsignadas} estudiante(s).";

                if (nombresNoEncontrados.Any())
                {
                    resumen += "\n\n❌ No se encontraron los siguientes nombres:\n" +
                               string.Join("\n", nombresNoEncontrados.Distinct());
                }

                MessageBox.Show(resumen, "Importación de Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al importar notas:\n" + ex.Message);
            }
        }

        private void btnImportarListas_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "Se importarán listas de estudiantes desde un archivo Excel en formato estandarizado.\n\n" +
                "🔁 Cada hoja será tratada como un curso\n" +
                "📍 Se leerán los nombres desde la celda B12)\n" +
                "Las siguientes acciones se realizarán automáticamente:\n" +
                "✅ Crear cursos si no existen\n" +
                "✅ Insertar estudiantes como activos en el curso correspondiente\n" +
                "✅ Evitar duplicados (no se insertarán estudiantes ya existentes)\n\n" +
                "¿Deseas continuar?",
                "Confirmar importación masiva",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes) return;

            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos Excel (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Selecciona archivo de Excel con listas de estudiantes"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var dataSet = reader.AsDataSet();

                int cursosNuevos = 0, cursosExistentes = 0;
                int estudiantesInsertados = 0, estudiantesOmitidos = 0;

                foreach (DataTable hoja in dataSet.Tables)
                {
                    string cursoNombre = hoja.TableName.Trim();
                    if (string.IsNullOrWhiteSpace(cursoNombre)) continue;

                    var curso = CourseRepository.GetByName(cursoNombre);
                    if (curso == null)
                    {
                        CourseRepository.Add(cursoNombre);
                        curso = CourseRepository.GetByName(cursoNombre);
                        cursosNuevos++;
                    }
                    else
                    {
                        cursosExistentes++;
                    }

                    for (int i = 11; i < hoja.Rows.Count; i++) // Fila 11 en Excel
                    {
                        string? nombre = hoja.Rows[i][1]?.ToString()?.Trim();

                        if (string.IsNullOrWhiteSpace(nombre)) continue;

                        // Aplicar filtros: mínimo 10 caracteres y máximo 3 guiones
                        if (nombre.Length < 10 || nombre.Count(c => c == '-') > 3) continue;

                        var existe = StudentRepository.Exists(nombre, curso.Id);
                        if (!existe)
                        {
                            StudentRepository.Add(nombre, curso.Id);
                            estudiantesInsertados++;
                        }
                        else
                        {
                            estudiantesOmitidos++;
                        }
                    }
                }

                MessageBox.Show(
                    $"✅ Importación completada:\n\n" +
                    $"Cursos nuevos: {cursosNuevos}\n" +
                    $"Cursos existentes: {cursosExistentes}\n" +
                    $"Estudiantes insertados: {estudiantesInsertados}\n" +
                    $"Estudiantes omitidos (ya existentes): {estudiantesOmitidos}",
                    "Resumen de importación", MessageBoxButtons.OK, MessageBoxIcon.Information
                );

                CargarCursos(); // Refrescar el ComboBox si es necesario

            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al importar listas:\n" + ex.Message);
            }
        }

        private void btnAdmMaterias_Click(object sender, EventArgs e)
        {
            var form = new FormMaterias();
            form.ShowDialog();
        }

        private void CargarMateriasDelCurso()
        {
            if (cursoSeleccionado == null) return;

            var materias = SubjectRepository.GetByCourseId(cursoSeleccionado.Id)
                .Where(m => m.Activo)
                .ToList();

            if (materias.Count == 0)
            {
                var respuesta = MessageBox.Show(
                    "Este curso no tiene materias activas.\n¿Deseas crear una ahora?",
                    "Sin materias",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (respuesta == DialogResult.Yes)
                {
                    var form = new FormMaterias();
                    form.ShowDialog();

                    // Intentar recargar materias luego de crear
                    materias = SubjectRepository.GetByCourseId(cursoSeleccionado.Id)
                        .Where(m => m.Activo)
                        .ToList();
                }
                else
                {
                    // ❌ El usuario no quiere crear materias: limpiar selección de curso
                    comboCursos.SelectedIndex = -1;
                    cursoSeleccionado = null;

                    cmbMateria.DataSource = null;
                    cmbMateria.Items.Clear();
                    materiaSeleccionada = null;

                    gridEstudiantes.Rows.Clear();
                    gridEstudiantes.Columns.Clear();
                    return;
                }
            }

            cmbMateria.DisplayMember = "Name";
            cmbMateria.ValueMember = "Id";
            cmbMateria.DataSource = materias;

            if (materias.Count > 0)
                cmbMateria.SelectedIndex = 0;
        }

        private Subject? materiaSeleccionada = null;

        private void cmbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMateria.SelectedItem is not Subject seleccionada) return;

            materiaSeleccionada = seleccionada;

            // Actualizar campos informativos
            txtInfoCurso.Text = cursoSeleccionado?.Name ?? "";
            txtInfoMateria.Text = materiaSeleccionada.Name;
            txtInfoDocente.Text = materiaSeleccionada.Teacher;
            txtInfoCantidad.Text = StudentRepository
                .GetByCourseId(cursoSeleccionado.Id)
                .Count(e => e.Activo)
                .ToString();

            CargarEstudiantes();
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (cursoSeleccionado == null || materiaSeleccionada == null)
            {
                MessageBox.Show("Selecciona un curso y una materia primero.");
                return;
            }

            using var saveFileDialog = new SaveFileDialog
            {
                Title = "Guardar reporte en PDF",
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                FileName = $"Reporte_{cursoSeleccionado.Name}_{materiaSeleccionada.Name}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var estudiantes = StudentRepository.GetByCourseId(cursoSeleccionado.Id)
                        .Where(e => e.Activo)
                        .ToList();

                    var doc = new ReporteNotasGenerator(
                        cursoSeleccionado,
                        materiaSeleccionada,
                        estudiantes,
                        studentId => GradeRepository.GetGradesByStudentAndSubject(studentId, materiaSeleccionada.Id));

                    doc.GenerarPDF(saveFileDialog.FileName);

                    MessageBox.Show("✅ PDF generado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al generar el reporte:\n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
