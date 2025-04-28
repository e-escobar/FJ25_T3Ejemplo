using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3Ejemplo.Modelos
{
    public class ModeloCursos
    {
        int id;
        string titulo;
        string instructor;
        string categoria;
        string dificultad;
        string estado;
        DateTime fecha_creacion;
        Decimal precio;
        int duracion_horas;
        string descripcion;

        public int Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Instructor { get => instructor; set => instructor = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
        public string Estado { get => estado; set => estado = value; }
        public DateTime Fecha_creacion { get => fecha_creacion; set => fecha_creacion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Duracion_horas { get => duracion_horas; set => duracion_horas = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
