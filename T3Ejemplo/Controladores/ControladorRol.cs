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
    internal class ControladorRol
    {
        public void consultaRoles(ComboBox cbRol)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloRoles objetoRol = new Modelos.ModeloRoles();
            DataTable dtModelo = new DataTable();
            dtModelo.Columns.Add("Id", typeof(int));
            dtModelo.Columns.Add("Nombre", typeof(string));
            string sql = @"SELECT id, nombre FROM roles;";
            try
            {
                MySqlConnection sqlConexion = conexion.establecerConexion();
                MySqlCommand comando = new MySqlCommand(sql, sqlConexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataSet ds = new DataSet();
                adaptador.Fill(ds);
                DataTable dtRoles = ds.Tables[0];
                foreach (DataRow row in dtRoles.Rows)
                {
                    objetoRol.Id = Convert.ToInt32(row["Id"]);
                    objetoRol.Nombre = row["Nombre"].ToString();
                    dtModelo.Rows.Add(objetoRol.Id, objetoRol.Nombre);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al consultar los roles: " + e.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }

            cbRol.DataSource = dtModelo;
            cbRol.ValueMember = "Id";
            cbRol.DisplayMember = "Nombre";


        }
    }
}
