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
    internal class ControladorCategoria
    {
        public void consultaCategorias(ComboBox cbCategoria)
        {
            Conexion.Conexion conexion = new Conexion.Conexion();
            Modelos.ModeloCategorias objetoCategoria = new Modelos.ModeloCategorias();
            DataTable dtModelo = new DataTable();

            dtModelo.Columns.Add("Id", typeof(int));
            dtModelo.Columns.Add("Nombre", typeof(string));
            
            string sql = @"SELECT * FROM categorias;";
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
                    objetoCategoria.Id = Convert.ToInt32(row["Id"]);
                    objetoCategoria.Nombre = row["Nombre"].ToString();
                    dtModelo.Rows.Add(objetoCategoria.Id, objetoCategoria.Nombre);
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
            
            cbCategoria.DataSource = dtModelo;
            cbCategoria.ValueMember = "Id";
            cbCategoria.DisplayMember = "Nombre";
        }


        
    }
}
