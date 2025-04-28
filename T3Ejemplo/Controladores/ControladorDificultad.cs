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
    internal class ControladorDificultad
    {
        public void consultaDificultad(ComboBox cbDificultad)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloDificultad objetoDificultad = new Modelos.ModeloDificultad();
            DataTable dtModelo = new DataTable();

            dtModelo.Columns.Add("Id", typeof(int));
            dtModelo.Columns.Add("Nombre", typeof(string));

            string sql = @"SELECT * FROM dificultad";
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
                    objetoDificultad.Id = Convert.ToInt32(row["Id"]);
                    objetoDificultad.Nombre = row["Nombre"].ToString();
                    dtModelo.Rows.Add(objetoDificultad.Id, objetoDificultad.Nombre);
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

            cbDificultad.DataSource = dtModelo;
            cbDificultad.ValueMember = "Id";
            cbDificultad.DisplayMember = "Nombre";
        }
    }
}
