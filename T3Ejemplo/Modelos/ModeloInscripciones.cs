using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3Ejemplo.Modelos
{
    internal class ModeloInscripciones
    {
        int id;
        int id_persona;
        int id_curso;
        DateTime fecha_inscripcion;
        int id_estado_inscripcion;

        public int Id { get => id; set => id = value; }
        public int Id_persona { get => id_persona; set => id_persona = value; }
        public int Id_curso { get => id_curso; set => id_curso = value; }
        public DateTime Fecha_inscripcion { get => fecha_inscripcion; set => fecha_inscripcion = value; }
        public int Id_estado_inscripcion { get => id_estado_inscripcion; set => id_estado_inscripcion = value; }
    }
}
