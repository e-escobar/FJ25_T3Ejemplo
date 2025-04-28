using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3Ejemplo.Modelos
{
    internal class ModeloPersonas
    {
        int id;
        string nombre;
        string email;
        string contrasena_hash;
        DateTime fecha_registro;
        string rol;
        string foto_perfil;
        string formato_imagen;
        string telefono;
        DateTime ultimo_login;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Email { get => email; set => email = value; }
        public string Contrasena_hash { get => contrasena_hash; set => contrasena_hash = value; }
        public DateTime Fecha_registro { get => fecha_registro; set => fecha_registro = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Foto_perfil { get => foto_perfil; set => foto_perfil = value; }
        public string Formato_imagen { get => formato_imagen; set => formato_imagen = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public DateTime Ultimo_login { get => ultimo_login; set => ultimo_login = value; }
    }
}
