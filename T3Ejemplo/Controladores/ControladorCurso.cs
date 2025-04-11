using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T3Ejemplo.Controladores
{
    internal class ControladorCurso
    {
        public void consultarCursos(DataGridView dgCursos) { 
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloCursos objetoCursos = new Modelos.ModeloCursos();
            DataTable dtCursos = new DataTable();

            dtCursos.Columns.Add("Id", typeof(int));
            dtCursos.Columns.Add("Titulo", typeof(string));
            dtCursos.Columns.Add("Instructor", typeof(string));
            dtCursos.Columns.Add("Categoria", typeof(string));
            dtCursos.Columns.Add("Dificultad", typeof(string));
            dtCursos.Columns.Add("Estado", typeof(string));
            dtCursos.Columns.Add("Fecha Creación", typeof(DateTime));
            dtCursos.Columns.Add("Precio", typeof(decimal));
            dtCursos.Columns.Add("Duración (horas)", typeof(int));
            dtCursos.Columns.Add("Descripción", typeof(string));

            string sql = @"SELECT c.id, c.titulo,
                            p.nombre,
                            cat.nombre,
                            d.nombre,
                            e.nombre,
                            c.fecha_creacion,
                            c.precio,
                            c.duracion_horas,
                            c.descripcion
                            FROM cursos c
                            INNER JOIN personas p ON c.id_instructor = p.id
                            INNER JOIN categorias cat ON c.id_categoria = cat.id
                            INNER JOIN dificultad d ON c.id_dificultad = d.id
                            INNER JOIN estados_curso e ON c.id_estado = e.id";

            try { 
                MySqlConnection sqlConnection = conexion.establecerConexion();
                MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                
                DataSet dt = new DataSet();
                sqlDataAdapter.Fill(dt);

                DataTable dtDatos = dt.Tables[0];

                foreach (DataRow row in dtDatos.Rows)
                {
                    objetoCursos.Id = Convert.ToInt32(row["id"]);
                    objetoCursos.Titulo = row["titulo"].ToString();
                    objetoCursos.Instructor = row["nombre"].ToString();
                    objetoCursos.Categoria = row["nombre"].ToString();
                    objetoCursos.Dificultad = row["nombre"].ToString();
                    objetoCursos.Estado = row["nombre"].ToString();
                    objetoCursos.Fecha_creacion = Convert.ToDateTime(row["fecha_creacion"]);
                    objetoCursos.Precio = Convert.ToDecimal(row["precio"]);
                    objetoCursos.Duracion_horas = Convert.ToInt32(row["duracion_horas"]);
                    objetoCursos.Descripcion = row["descripcion"].ToString();
                    dtCursos.Rows.Add(objetoCursos.Id, objetoCursos.Titulo, objetoCursos.Instructor, objetoCursos.Categoria,
                        objetoCursos.Dificultad, objetoCursos.Estado, objetoCursos.Fecha_creacion, objetoCursos.Precio,
                        objetoCursos.Duracion_horas, objetoCursos.Descripcion);
                }

                dgCursos.DataSource = dtCursos;

            }
            catch (Exception ex) {

                MessageBox.Show("Error al consultar los cursos: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.cerrarConexion();

                }
            }

        }
    }
}
