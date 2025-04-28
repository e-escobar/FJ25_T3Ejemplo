using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace T3Ejemplo.Controladores
{
    internal class ControladorEstadosCurso
    {
        public void consultaEstadosCurso(ComboBox cbEstadosCurso)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloEstadosCursos objetoEstadosCurso = new Modelos.ModeloEstadosCursos();
            DataTable dtModelo = new DataTable();
            dtModelo.Columns.Add("Id", typeof(int));
            dtModelo.Columns.Add("Nombre", typeof(string));
            string sql = @"SELECT * FROM estados_curso";
            DataTable dt = dtModelo;
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataSet ds = new DataSet();
                adaptador.Fill(ds);
                DataTable dtCategorias = ds.Tables[0];
                foreach (DataRow row in dtCategorias.Rows)
                {
                    objetoEstadosCurso.Id = Convert.ToInt32(row["Id"]);
                    objetoEstadosCurso.Nombre = row["Nombre"].ToString();
                    dtModelo.Rows.Add(objetoEstadosCurso.Id, objetoEstadosCurso.Nombre);
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
            cbEstadosCurso.DataSource = dtModelo;
            cbEstadosCurso.ValueMember = "Id";
            cbEstadosCurso.DisplayMember = "Nombre";
        }
    }
}
