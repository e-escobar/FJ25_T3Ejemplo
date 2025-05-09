﻿using MySql.Data.MySqlClient;
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

    }
}
