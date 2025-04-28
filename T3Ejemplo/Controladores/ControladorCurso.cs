using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using T3Ejemplo.Modelos;

namespace T3Ejemplo.Controladores
{
    internal class ControladorCurso
    {
        public void consultarCursos(DataGridView dgCursos)
        {
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
                            p.nombre AS 'Instructor',
                            cat.nombre AS 'Categoria',
                            d.nombre AS 'Dificultad',
                            e.nombre AS 'Estado',
                            c.fecha_creacion,
                            c.precio,
                            c.duracion_horas,
                            c.descripcion
                            FROM cursos c
                            INNER JOIN personas p ON c.id_instructor = p.id
                            INNER JOIN categorias cat ON c.id_categoria = cat.id
                            INNER JOIN dificultad d ON c.id_dificultad = d.id
                            INNER JOIN estados_curso e ON c.id_estado = e.id";

            try
            {
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
                    objetoCursos.Instructor = row["Instructor"].ToString();
                    objetoCursos.Categoria = row["Categoria"].ToString();
                    objetoCursos.Dificultad = row["Dificultad"].ToString();
                    objetoCursos.Estado = row["Estado"].ToString();
                    objetoCursos.Fecha_creacion = Convert.ToDateTime(row["fecha_creacion"]);
                    objetoCursos.Precio = Convert.ToDecimal(row["precio"]);
                    objetoCursos.Duracion_horas = Convert.ToInt32(row["duracion_horas"]);
                    objetoCursos.Descripcion = row["descripcion"].ToString();
                    dtCursos.Rows.Add(objetoCursos.Id, objetoCursos.Titulo, objetoCursos.Instructor, objetoCursos.Categoria,
                        objetoCursos.Dificultad, objetoCursos.Estado, objetoCursos.Fecha_creacion, objetoCursos.Precio,
                        objetoCursos.Duracion_horas, objetoCursos.Descripcion);
                }

                dgCursos.DataSource = dtCursos; // asiganar los cursos al DataGridView

            }
            catch (Exception ex)
            {
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

        public void cursosPorId(int idCurso, ModeloCursos objetoCursos)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            string sql = @"SELECT c.id, c.titulo,
                            p.nombre AS 'Instructor',
                            cat.nombre AS 'Categoria',
                            d.nombre AS 'Dificultad',
                            e.nombre AS 'Estado',
                            c.fecha_creacion,
                            c.precio,
                            c.duracion_horas,
                            c.descripcion
                            FROM cursos c
                            INNER JOIN personas p ON c.id_instructor = p.id
                            INNER JOIN categorias cat ON c.id_categoria = cat.id
                            INNER JOIN dificultad d ON c.id_dificultad = d.id
                            INNER JOIN estados_curso e ON c.id_estado = e.id
                            WHERE c.id = @idCurso";
            try
            {
                MySqlConnection sqlConnection = conexion.establecerConexion();
                MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@idCurso", idCurso);
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                DataSet dt = new DataSet();
                sqlDataAdapter.Fill(dt);
                DataTable dtDatos = dt.Tables[0];
                foreach (DataRow row in dtDatos.Rows)
                {
                    objetoCursos.Id = Convert.ToInt32(row["id"]);
                    objetoCursos.Titulo = row["titulo"].ToString();
                    objetoCursos.Instructor = row["Instructor"].ToString();
                    objetoCursos.Categoria = row["Categoria"].ToString();
                    objetoCursos.Dificultad = row["Dificultad"].ToString();
                    objetoCursos.Estado = row["Estado"].ToString();
                    objetoCursos.Fecha_creacion = Convert.ToDateTime(row["fecha_creacion"]);
                    objetoCursos.Precio = Convert.ToDecimal(row["precio"]);
                    objetoCursos.Duracion_horas = Convert.ToInt32(row["duracion_horas"]);
                    objetoCursos.Descripcion = row["descripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar el curso: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.cerrarConexion();
                }
            }
        }

        public void agregarCurso(ModeloCursos objetoCursos)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            string sql = @"INSERT INTO cursos (titulo, id_instructor, id_categoria, id_dificultad, id_estado, precio, duracion_horas, descripcion) 
                            VALUES (@titulo, @id_instructor, @id_categoria, @id_dificultad, @id_estado, @precio, @duracion_horas, @descripcion);";
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                comando.Parameters.AddWithValue("@titulo", objetoCursos.Titulo);
                comando.Parameters.AddWithValue("@id_instructor", objetoCursos.Instructor);
                comando.Parameters.AddWithValue("@id_categoria", objetoCursos.Categoria);
                comando.Parameters.AddWithValue("@id_dificultad", objetoCursos.Dificultad);
                comando.Parameters.AddWithValue("@id_estado", objetoCursos.Estado);
                comando.Parameters.AddWithValue("@precio", objetoCursos.Precio);
                comando.Parameters.AddWithValue("@duracion_horas", objetoCursos.Duracion_horas);
                comando.Parameters.AddWithValue("@descripcion", objetoCursos.Descripcion);
                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Curso agregado correctamente.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al agregar el curso: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
        
        public void actualizarCurso(ModeloCursos objetoCursos)
        {
            if (objetoCursos == null)
            {
                MessageBox.Show("El objeto curso no puede ser nulo.");
                return;
            }
            Conexion.Conexion conexion = new Conexion.Conexion();
            string sql = @"UPDATE cursos SET 
                            titulo = @titulo,
                            id_instructor = @id_instructor,
                            id_categoria = @id_categoria,
                            id_dificultad = @id_dificultad,
                            id_estado = @id_estado,
                            precio = @precio,
                            duracion_horas = @duracion_horas,
                            descripcion = @descripcion
                            WHERE id = @id";
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                comando.Parameters.AddWithValue("@titulo", objetoCursos.Titulo);
                comando.Parameters.AddWithValue("@id_instructor", Convert.ToInt32(objetoCursos.Instructor));
                comando.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(objetoCursos.Categoria));
                comando.Parameters.AddWithValue("@id_dificultad", Convert.ToInt32(objetoCursos.Dificultad));
                comando.Parameters.AddWithValue("@id_estado", Convert.ToInt32(objetoCursos.Estado));
                comando.Parameters.AddWithValue("@precio", objetoCursos.Precio);
                comando.Parameters.AddWithValue("@duracion_horas", objetoCursos.Duracion_horas);
                comando.Parameters.AddWithValue("@descripcion", objetoCursos.Descripcion);
                comando.Parameters.AddWithValue("@id", objetoCursos.Id);
                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Curso actualizado correctamente.");
                }
                else
                {
                    MessageBox.Show("No se encontró el curso a actualizar.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al actualizar el curso: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void eliminarCurso(int idCurso)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            string sql = @"DELETE FROM cursos WHERE id = @idCurso";
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                comando.Parameters.AddWithValue("@idCurso", idCurso);
                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Curso eliminado correctamente.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al eliminar el curso: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
    }
}
