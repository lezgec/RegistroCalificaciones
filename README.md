# 📘 Sistema de Registro de Calificaciones

Este proyecto es una aplicación de escritorio desarrollada en **C# con WinForms** y **SQLite** para gestionar cursos, materias, estudiantes y sus calificaciones. Está diseñado para facilitar el registro, importación y exportación de notas de forma flexible y personalizable.

---

## 🛠 Tecnologías Utilizadas

- C# (.NET Windows Forms)
- SQLite (base de datos local)
- Microsoft Excel (interoperabilidad)
- QuestPDF (para futura generación de reportes PDF)

---

## 🎯 Funcionalidades Principales

- ✅ Gestión de cursos, materias y estudiantes.
- ✅ Registro de calificaciones personalizables por materia.
- ✅ Importación masiva de listas desde Excel.
- ✅ Exportación de notas a Excel con encabezado personalizado (`header.jpg`).
- ✅ Búsqueda, edición y eliminación lógica de estudiantes.
- ✅ Cálculo automático de promedios.
- ✅ Vista previa para impresión y generación de reportes (en desarrollo).

---

## 📂 Estructura del Proyecto

```
/Resources/
 └── header.jpg          # Imagen de encabezado para reportes exportados
/Helpers/
 └── DatabaseHelper.cs   # Clase para conexión y creación de la base de datos
/Forms/
 ├── Form1.cs            # Vista principal con gestión de notas
 ├── FormMaterias.cs     # Gestión de materias por curso
 └── ...
calificaciones.db        # Base de datos local generada al iniciar
```

---

## 📥 Instalación y Uso

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/tuusuario/nombre-repo.git
   ```

2. **Abrir en Visual Studio** y compilar.

3. **Ejecutar** el archivo `.exe` generado en `bin/Debug` o `Release`.

4. El archivo `calificaciones.db` se crea automáticamente al iniciar si no existe.

---

## 📊 Personalización

- Puedes cambiar la imagen del encabezado exportado reemplazando `Resources/header.jpg`.
- Las columnas de notas se pueden agregar o eliminar según la materia.
- Totalmente offline: no requiere conexión a internet.

---

## 🧑‍💻 Autor

- **Luis Zamora**  
  Portafolio: [www.luiszamora.dev](https://www.luiszamora.dev)  
  Correo: certs@luiszamora.dev

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Puedes usarlo, modificarlo y distribuirlo libremente.
