using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroCalificaciones.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Teacher { get; set; } = "";
        public int CourseId { get; set; }
        public bool Activo { get; set; } = true;
    }

}
