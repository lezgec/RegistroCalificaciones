using Microsoft.Data.Sqlite;
using RegistroCalificaciones.Helpers;
using RegistroCalificaciones.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroCalificaciones
{
    public partial class FormMaterias : Form
    {
        public FormMaterias()
        {
            InitializeComponent();
            InicializarColumnasGrid();
            this.Text = "Agregar Materias";
            this.Icon = new Icon("Resources/logo.ico");
            CargarCursos();
        }

        private void CargarCursos()
        {
            cmbCurso.Items.Clear();

            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name FROM Courses WHERE Activo = 1";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                cmbCurso.Items.Add(new ComboBoxItem
                {
                    Text = reader.GetString(1),
                    Value = reader.GetInt32(0)
                });
            }

            if (cmbCurso.Items.Count > 0)
                cmbCurso.SelectedIndex = 0;
        }

        private void cmbCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMaterias();
        }

        private void CargarMaterias()
        {
            gridMaterias.Rows.Clear();

            if (cmbCurso.SelectedItem is not ComboBoxItem selected) return;

            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, Teacher, Activo FROM Subjects WHERE CourseId = @courseId";
            command.Parameters.AddWithValue("@courseId", selected.Value);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                gridMaterias.Rows.Add(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.IsDBNull(2) ? "" : reader.GetString(2),
                    reader.GetInt32(3) == 1
                );
            }
        }

        private void btnAgregarMateria_Click(object sender, EventArgs e)
        {
            gridMaterias.Rows.Add(0, "", "", true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbCurso.SelectedItem is not ComboBoxItem selected) return;

            var respuesta = MessageBox.Show(
                "¿Deseas guardar esta(s) materia(s) para todos los cursos?",
                "Guardar materia",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Cancel) return;

            var cursos = CourseRepository.GetAll();
            using var connection = new SqliteConnection(DatabaseHelper.ConnectionString);
            connection.Open();

            foreach (DataGridViewRow row in gridMaterias.Rows)
            {
                if (row.IsNewRow) continue;

                int id = Convert.ToInt32(row.Cells["ColId"].Value ?? 0);
                string nombre = row.Cells["ColNombre"].Value?.ToString()?.Trim() ?? "";
                string docente = row.Cells["ColDocente"].Value?.ToString()?.Trim() ?? "";
                bool activo = Convert.ToBoolean(row.Cells["ColActivo"].Value ?? true);

                if (string.IsNullOrWhiteSpace(nombre)) continue;

                foreach (var curso in cursos)
                {
                    // Si eligió "No", solo aplica al curso seleccionado
                    if (respuesta == DialogResult.No && curso.Id != (int)selected.Value)
                        continue;

                    using var cmd = connection.CreateCommand();

                    // Verificar si ya existe una materia con el mismo nombre en el curso
                    cmd.CommandText = "SELECT COUNT(*) FROM Subjects WHERE Name = @name AND CourseId = @courseId";
                    cmd.Parameters.AddWithValue("@name", nombre);
                    cmd.Parameters.AddWithValue("@courseId", curso.Id);
                    int existe = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Parameters.Clear();

                    if (id > 0 && curso.Id == (int)selected.Value)
                    {
                        // Actualizar solo si ya existe y es del curso actual
                        cmd.CommandText = @"
                    UPDATE Subjects
                    SET Name = @name, Teacher = @teacher, Activo = @activo
                    WHERE Id = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    else if (existe == 0)
                    {
                        // Insertar si no existe en este curso
                        cmd.CommandText = @"
                    INSERT INTO Subjects (Name, Teacher, CourseId, Activo)
                    VALUES (@name, @teacher, @courseId, @activo)";
                    }
                    else
                    {
                        continue; // Ya existe, no hacer nada
                    }

                    // Parámetros comunes
                    cmd.Parameters.AddWithValue("@name", nombre);
                    cmd.Parameters.AddWithValue("@teacher", docente);
                    cmd.Parameters.AddWithValue("@courseId", curso.Id);
                    cmd.Parameters.AddWithValue("@activo", activo ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("✅ Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarMaterias();
        }



        private void InicializarColumnasGrid()
        {
            gridMaterias.Columns.Clear();

            gridMaterias.Columns.Add("ColId", "ID");
            gridMaterias.Columns["ColId"].Visible = false;

            gridMaterias.Columns.Add("ColNombre", "Materia");
            gridMaterias.Columns.Add("ColDocente", "Docente");

            var colActivo = new DataGridViewCheckBoxColumn
            {
                Name = "ColActivo",
                HeaderText = "Activo",
                Width = 60
            };
            gridMaterias.Columns.Add(colActivo);
        }
       
    }
}
