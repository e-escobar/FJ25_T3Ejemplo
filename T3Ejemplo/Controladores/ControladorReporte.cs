using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T3Ejemplo.Controladores
{
    internal class ControladorReporte
    {
        public void reporteInscripcionesPorCurso(DataSet dsReporte)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();

            string sql = @"SELECT 
                            c.titulo AS 'Título',
                            p.nombre AS 'Instructor',
                            cat.nombre AS 'Categoría',
                            d.nombre AS 'Dificultad',
                            ec.nombre AS 'Estado',
                            COUNT(i.id_curso) AS 'Inscritos'
                        FROM cursos c
                        INNER JOIN personas p ON c.id_instructor = p.id
                        INNER JOIN categorias cat ON c.id_categoria = cat.id
                        INNER JOIN dificultad d ON c.id_dificultad = d.id
                        INNER JOIN estados_curso ec ON c.id_estado = ec.id
                        INNER JOIN inscripciones i ON c.id = i.id_curso
                        GROUP BY c.id;";


            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);

                adaptador.Fill(dsReporte);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al mostrar los cursos: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void reportePersonas(DataSet dsReporte)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();

            string sql = @"SELECT 
                            r.nombre AS 'Rol',
                            p.nombre AS 'Nombre',
                            p.email AS 'Email',
                            p.telefono AS 'Teléfono'
                        FROM personas p
                        inner JOIN roles r ON p.id_rol = r.id";


            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);

                adaptador.Fill(dsReporte);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al mostrar las personas: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
    }
}
