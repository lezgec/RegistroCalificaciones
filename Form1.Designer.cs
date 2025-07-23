namespace RegistroCalificaciones
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gridEstudiantes = new DataGridView();
            splitContainer1 = new SplitContainer();
            txtInfoCantidad = new TextBox();
            txtInfoDocente = new TextBox();
            txtInfoMateria = new TextBox();
            txtInfoCurso = new TextBox();
            lblCantidadAlumnos = new Label();
            lblProfesor = new Label();
            lblMateria = new Label();
            lblCurso = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox4 = new GroupBox();
            cmbMateria = new ComboBox();
            btnEliminarCurso = new Button();
            comboCursos = new ComboBox();
            btnAgregarCurso = new Button();
            groupBox5 = new GroupBox();
            btnAdmMaterias = new Button();
            btnLimpiarFiltro = new Button();
            txtBuscar = new TextBox();
            groupBox1 = new GroupBox();
            btnEliminarEstudiante = new Button();
            btnAgregarEstudiante = new Button();
            groupBox2 = new GroupBox();
            btnImportarListas = new Button();
            btnImportarNotas = new Button();
            btnExportarExcel = new Button();
            btnImportarEstudiantes = new Button();
            groupBox3 = new GroupBox();
            btnGenerarPDF = new Button();
            ((System.ComponentModel.ISupportInitialize)gridEstudiantes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // gridEstudiantes
            // 
            gridEstudiantes.AllowUserToAddRows = false;
            gridEstudiantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEstudiantes.Dock = DockStyle.Fill;
            gridEstudiantes.Location = new Point(0, 0);
            gridEstudiantes.Name = "gridEstudiantes";
            gridEstudiantes.Size = new Size(800, 282);
            gridEstudiantes.TabIndex = 3;
            gridEstudiantes.CellValueChanged += gridEstudiantes_CellValueChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtInfoCantidad);
            splitContainer1.Panel1.Controls.Add(txtInfoDocente);
            splitContainer1.Panel1.Controls.Add(txtInfoMateria);
            splitContainer1.Panel1.Controls.Add(txtInfoCurso);
            splitContainer1.Panel1.Controls.Add(lblCantidadAlumnos);
            splitContainer1.Panel1.Controls.Add(lblProfesor);
            splitContainer1.Panel1.Controls.Add(lblMateria);
            splitContainer1.Panel1.Controls.Add(lblCurso);
            splitContainer1.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(gridEstudiantes);
            splitContainer1.Size = new Size(800, 476);
            splitContainer1.SplitterDistance = 190;
            splitContainer1.TabIndex = 8;
            // 
            // txtInfoCantidad
            // 
            txtInfoCantidad.Location = new Point(736, 162);
            txtInfoCantidad.Name = "txtInfoCantidad";
            txtInfoCantidad.ReadOnly = true;
            txtInfoCantidad.Size = new Size(39, 23);
            txtInfoCantidad.TabIndex = 15;
            // 
            // txtInfoDocente
            // 
            txtInfoDocente.Location = new Point(428, 161);
            txtInfoDocente.Name = "txtInfoDocente";
            txtInfoDocente.ReadOnly = true;
            txtInfoDocente.Size = new Size(200, 23);
            txtInfoDocente.TabIndex = 14;
            // 
            // txtInfoMateria
            // 
            txtInfoMateria.Location = new Point(209, 161);
            txtInfoMateria.Name = "txtInfoMateria";
            txtInfoMateria.ReadOnly = true;
            txtInfoMateria.Size = new Size(152, 23);
            txtInfoMateria.TabIndex = 13;
            // 
            // txtInfoCurso
            // 
            txtInfoCurso.Location = new Point(47, 161);
            txtInfoCurso.Name = "txtInfoCurso";
            txtInfoCurso.ReadOnly = true;
            txtInfoCurso.Size = new Size(100, 23);
            txtInfoCurso.TabIndex = 12;
            // 
            // lblCantidadAlumnos
            // 
            lblCantidadAlumnos.AutoSize = true;
            lblCantidadAlumnos.Location = new Point(675, 166);
            lblCantidadAlumnos.Name = "lblCantidadAlumnos";
            lblCantidadAlumnos.Size = new Size(58, 15);
            lblCantidadAlumnos.TabIndex = 11;
            lblCantidadAlumnos.Text = "Alumnos:";
            // 
            // lblProfesor
            // 
            lblProfesor.AutoSize = true;
            lblProfesor.Location = new Point(371, 165);
            lblProfesor.Name = "lblProfesor";
            lblProfesor.Size = new Size(57, 15);
            lblProfesor.TabIndex = 10;
            lblProfesor.Text = "Profesor: ";
            // 
            // lblMateria
            // 
            lblMateria.AutoSize = true;
            lblMateria.Location = new Point(160, 164);
            lblMateria.Name = "lblMateria";
            lblMateria.Size = new Size(53, 15);
            lblMateria.TabIndex = 9;
            lblMateria.Text = "Materia: ";
            // 
            // lblCurso
            // 
            lblCurso.AutoSize = true;
            lblCurso.Location = new Point(7, 164);
            lblCurso.Name = "lblCurso";
            lblCurso.Size = new Size(41, 15);
            lblCurso.TabIndex = 8;
            lblCurso.Text = "Curso:";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Top;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 160);
            tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox5);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 132);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Activos";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(cmbMateria);
            groupBox4.Controls.Add(btnEliminarCurso);
            groupBox4.Controls.Add(comboCursos);
            groupBox4.Controls.Add(btnAgregarCurso);
            groupBox4.Dock = DockStyle.Left;
            groupBox4.Location = new Point(3, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(163, 126);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Cursos";
            // 
            // cmbMateria
            // 
            cmbMateria.FormattingEnabled = true;
            cmbMateria.Location = new Point(3, 48);
            cmbMateria.Name = "cmbMateria";
            cmbMateria.Size = new Size(154, 23);
            cmbMateria.TabIndex = 3;
            cmbMateria.Text = "Selecciona una materia";
            cmbMateria.SelectedIndexChanged += cmbMateria_SelectedIndexChanged;
            // 
            // btnEliminarCurso
            // 
            btnEliminarCurso.AutoSize = true;
            btnEliminarCurso.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnEliminarCurso.Dock = DockStyle.Bottom;
            btnEliminarCurso.Location = new Point(3, 73);
            btnEliminarCurso.Name = "btnEliminarCurso";
            btnEliminarCurso.Size = new Size(157, 25);
            btnEliminarCurso.TabIndex = 2;
            btnEliminarCurso.Text = "Eliminar Curso";
            btnEliminarCurso.UseVisualStyleBackColor = true;
            btnEliminarCurso.Click += btnEliminarCurso_Click;
            // 
            // comboCursos
            // 
            comboCursos.Dock = DockStyle.Fill;
            comboCursos.FormattingEnabled = true;
            comboCursos.Location = new Point(3, 19);
            comboCursos.Name = "comboCursos";
            comboCursos.Size = new Size(157, 23);
            comboCursos.TabIndex = 0;
            comboCursos.Text = "Selecciona un Curso";
            comboCursos.SelectedIndexChanged += comboCursos_SelectedIndexChanged;
            comboCursos.Click += comboCursos_SelectedIndexChanged;
            // 
            // btnAgregarCurso
            // 
            btnAgregarCurso.AutoSize = true;
            btnAgregarCurso.Dock = DockStyle.Bottom;
            btnAgregarCurso.Location = new Point(3, 98);
            btnAgregarCurso.Name = "btnAgregarCurso";
            btnAgregarCurso.Size = new Size(157, 25);
            btnAgregarCurso.TabIndex = 1;
            btnAgregarCurso.Text = "Agregar Curso";
            btnAgregarCurso.UseVisualStyleBackColor = true;
            btnAgregarCurso.Click += btnAgregarCurso_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnAdmMaterias);
            groupBox5.Controls.Add(btnLimpiarFiltro);
            groupBox5.Controls.Add(txtBuscar);
            groupBox5.Dock = DockStyle.Right;
            groupBox5.Location = new Point(321, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(151, 126);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Buscar";
            // 
            // btnAdmMaterias
            // 
            btnAdmMaterias.Location = new Point(2, 65);
            btnAdmMaterias.Name = "btnAdmMaterias";
            btnAdmMaterias.Size = new Size(143, 23);
            btnAdmMaterias.TabIndex = 7;
            btnAdmMaterias.Text = "Administrar Materias";
            btnAdmMaterias.UseVisualStyleBackColor = true;
            btnAdmMaterias.Click += btnAdmMaterias_Click;
            // 
            // btnLimpiarFiltro
            // 
            btnLimpiarFiltro.Location = new Point(112, 22);
            btnLimpiarFiltro.Name = "btnLimpiarFiltro";
            btnLimpiarFiltro.Size = new Size(21, 23);
            btnLimpiarFiltro.TabIndex = 8;
            btnLimpiarFiltro.Text = "X";
            btnLimpiarFiltro.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(6, 22);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            txtBuscar.TabIndex = 7;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnEliminarEstudiante);
            groupBox1.Controls.Add(btnAgregarEstudiante);
            groupBox1.Dock = DockStyle.Right;
            groupBox1.Location = new Point(472, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(155, 126);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Estudiantes";
            // 
            // btnEliminarEstudiante
            // 
            btnEliminarEstudiante.AutoSize = true;
            btnEliminarEstudiante.Dock = DockStyle.Top;
            btnEliminarEstudiante.Location = new Point(3, 42);
            btnEliminarEstudiante.Name = "btnEliminarEstudiante";
            btnEliminarEstudiante.Size = new Size(149, 25);
            btnEliminarEstudiante.TabIndex = 3;
            btnEliminarEstudiante.Text = "Eliminar Estudiante";
            btnEliminarEstudiante.UseVisualStyleBackColor = true;
            btnEliminarEstudiante.Click += btnEliminarEstudiante_Click;
            // 
            // btnAgregarEstudiante
            // 
            btnAgregarEstudiante.Dock = DockStyle.Top;
            btnAgregarEstudiante.Location = new Point(3, 19);
            btnAgregarEstudiante.Name = "btnAgregarEstudiante";
            btnAgregarEstudiante.Size = new Size(149, 23);
            btnAgregarEstudiante.TabIndex = 2;
            btnAgregarEstudiante.Text = "Agregar Estudiante";
            btnAgregarEstudiante.UseVisualStyleBackColor = true;
            btnAgregarEstudiante.Click += btnAgregarEstudiante_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnImportarListas);
            groupBox2.Controls.Add(btnImportarNotas);
            groupBox2.Controls.Add(btnExportarExcel);
            groupBox2.Controls.Add(btnImportarEstudiantes);
            groupBox2.Dock = DockStyle.Right;
            groupBox2.Location = new Point(627, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(162, 126);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Importar y Exportar";
            // 
            // btnImportarListas
            // 
            btnImportarListas.Dock = DockStyle.Top;
            btnImportarListas.Location = new Point(3, 88);
            btnImportarListas.Name = "btnImportarListas";
            btnImportarListas.Size = new Size(156, 23);
            btnImportarListas.TabIndex = 7;
            btnImportarListas.Text = "Importar Listas(Masivo)";
            btnImportarListas.UseVisualStyleBackColor = true;
            btnImportarListas.Click += btnImportarListas_Click;
            // 
            // btnImportarNotas
            // 
            btnImportarNotas.Dock = DockStyle.Top;
            btnImportarNotas.Location = new Point(3, 65);
            btnImportarNotas.Name = "btnImportarNotas";
            btnImportarNotas.Size = new Size(156, 23);
            btnImportarNotas.TabIndex = 6;
            btnImportarNotas.Text = "Importar Notas";
            btnImportarNotas.UseVisualStyleBackColor = true;
            btnImportarNotas.Click += btnImportarNotas_Click;
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.Dock = DockStyle.Top;
            btnExportarExcel.Location = new Point(3, 42);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(156, 23);
            btnExportarExcel.TabIndex = 5;
            btnExportarExcel.Text = "Exportar Notas";
            btnExportarExcel.UseVisualStyleBackColor = true;
            btnExportarExcel.Click += btnExportarExcel_Click;
            // 
            // btnImportarEstudiantes
            // 
            btnImportarEstudiantes.Dock = DockStyle.Top;
            btnImportarEstudiantes.Location = new Point(3, 19);
            btnImportarEstudiantes.Name = "btnImportarEstudiantes";
            btnImportarEstudiantes.Size = new Size(156, 23);
            btnImportarEstudiantes.TabIndex = 0;
            btnImportarEstudiantes.Text = "Importar Listas";
            btnImportarEstudiantes.UseVisualStyleBackColor = true;
            btnImportarEstudiantes.Click += btnImportarEstudiantes_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnGenerarPDF);
            groupBox3.Location = new Point(172, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(145, 126);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Vista Previa e Impresion";
            // 
            // btnGenerarPDF
            // 
            btnGenerarPDF.Location = new Point(6, 42);
            btnGenerarPDF.Name = "btnGenerarPDF";
            btnGenerarPDF.Size = new Size(122, 23);
            btnGenerarPDF.TabIndex = 0;
            btnGenerarPDF.Text = "Generar PDF";
            btnGenerarPDF.UseVisualStyleBackColor = true;
            btnGenerarPDF.Click += btnGenerarPDF_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 476);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)gridEstudiantes).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DataGridView gridEstudiantes;
        private SplitContainer splitContainer1;
        private TabPage tabPage1;
        private GroupBox groupBox4;
        private Button btnEliminarCurso;
        private ComboBox comboCursos;
        private Button btnAgregarCurso;
        private GroupBox groupBox5;
        private Button btnLimpiarFiltro;
        private TextBox txtBuscar;
        private GroupBox groupBox1;
        private Button btnEliminarEstudiante;
        private Button btnAgregarEstudiante;
        private GroupBox groupBox2;
        private Button btnImportarListas;
        private Button btnImportarNotas;
        private Button btnExportarExcel;
        private Button btnImportarEstudiantes;
        private TabControl tabControl1;
        private ComboBox cmbMateria;
        private Button btnAdmMaterias;
        private TextBox txtInfoCantidad;
        private TextBox txtInfoDocente;
        private TextBox txtInfoMateria;
        private TextBox txtInfoCurso;
        private Label lblCantidadAlumnos;
        private Label lblProfesor;
        private Label lblMateria;
        private Label lblCurso;
        private GroupBox groupBox3;
        private Button btnGenerarPDF;
    }
}
