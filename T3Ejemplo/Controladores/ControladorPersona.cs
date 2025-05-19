using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T3Ejemplo.Controladores
{
    internal class ControladorPersona
    {
        public void consultaInstructores(ComboBox cbInstructores)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloPersonas objetoInstructores = new Modelos.ModeloPersonas();
            DataTable dtModelo = new DataTable();

            dtModelo.Columns.Add("Id", typeof(int));
            dtModelo.Columns.Add("Nombre", typeof(string));

            string sql = @"SELECT id, nombre FROM personas WHERE id_rol = 2;";

            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);

                DataSet ds = new DataSet();
                adaptador.Fill(ds);
                DataTable dtInstructores = ds.Tables[0];

                foreach (DataRow row in dtInstructores.Rows)
                {
                    objetoInstructores.Id = Convert.ToInt32(row["Id"]);
                    objetoInstructores.Nombre = row["Nombre"].ToString();
                    dtModelo.Rows.Add(objetoInstructores.Id, objetoInstructores.Nombre);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al consultar las categorias: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }

            cbInstructores.DataSource = dtModelo;
            cbInstructores.ValueMember = "Id";
            cbInstructores.DisplayMember = "Nombre";
        }

        public void consultarPersonas(DataGridView dgPersonas)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloPersonas objetoPersona = new Modelos.ModeloPersonas();
            DataTable dtModelo = new DataTable();
            dtModelo.Columns.Add("Id", typeof(int));
            dtModelo.Columns.Add("Nombre", typeof(string));
            dtModelo.Columns.Add("Email", typeof(string));
            dtModelo.Columns.Add("Telefono", typeof(string));
            dtModelo.Columns.Add("Rol", typeof(string));
            string sql = @"SELECT p.id, p.nombre, p.email, p.telefono, r.nombre AS rol
                           FROM personas p
                           INNER JOIN roles r ON p.id_rol = r.id;";
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataSet ds = new DataSet();
                adaptador.Fill(ds);
                DataTable dtPersonas = ds.Tables[0];
                foreach (DataRow row in dtPersonas.Rows)
                {
                    objetoPersona.Id = Convert.ToInt32(row["Id"]);
                    objetoPersona.Nombre = row["Nombre"].ToString();
                    objetoPersona.Email = row["Email"].ToString();
                    objetoPersona.Telefono = row["Telefono"].ToString();
                    objetoPersona.Rol = row["Rol"].ToString();
                    dtModelo.Rows.Add(objetoPersona.Id, objetoPersona.Nombre, objetoPersona.Email, objetoPersona.Telefono, objetoPersona.Rol);
                }
            
                dgPersonas.DataSource = dtModelo;
    
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al consultar las personas: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }

        }

        public void consultarFotoPersona(int id, PictureBox pbFoto)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            string sql = @"SELECT foto_perfil FROM personas WHERE id = @id;";
            try
            {
                using (MySqlConnection sqlConexion = conexion.establecerConexion())
                {
                    MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                    comando.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        string nombreImagen = reader["foto_perfil"].ToString();

                        if (!string.IsNullOrEmpty(nombreImagen))
                        {
                            string subcarpeta = "perfil";
                            string rutaCompleta = Path.Combine(Application.StartupPath, subcarpeta, nombreImagen);

                            if (File.Exists(rutaCompleta))
                            {
                                pbFoto.Image = Image.FromFile(rutaCompleta);
                            }
                            else
                            {
                                //pbFoto.Image = Properties.Resources.imagen_por_defecto; // Usar un recurso
                                MessageBox.Show($"La imagen no existe en: {rutaCompleta}");
                            }
                        }
                        else
                        {
                            pbFoto.Image = null; 
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Archivo no encontrado: {ex.FileName}");
                //pbFoto.Image = Properties.Resources.imagen_por_defecto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen: {ex.Message}");
            }
            finally
            {
                conexion.cerrarConexion();
            }

        }

        public void registrarPersona(Modelos.ModeloPersonas persona) {
            Conexion.Conexion conexion = new Conexion.Conexion();
            string sql = @"INSERT INTO personas (nombre, email, contrasena_hash, fecha_registro, id_rol, foto_perfil, formato_imagen, telefono)
                           VALUES (@nombre, @email, @contrasena_hash, @fecha_registro, @id_rol, @foto_perfil, @formato_imagen, @telefono);";
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                comando.Parameters.AddWithValue("@nombre", persona.Nombre);
                comando.Parameters.AddWithValue("@email", persona.Email);
                comando.Parameters.AddWithValue("@contrasena_hash", persona.Contrasena_hash);
                comando.Parameters.AddWithValue("@fecha_registro", persona.Fecha_registro);
                comando.Parameters.AddWithValue("@id_rol", persona.Rol);
                comando.Parameters.AddWithValue("@foto_perfil", persona.Foto_perfil);
                comando.Parameters.AddWithValue("@formato_imagen", persona.Formato_imagen);
                comando.Parameters.AddWithValue("@telefono", persona.Telefono);
                int filasAfectadas = comando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Persona registrada exitosamente.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al registrar la persona: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }

        }

    }
}
