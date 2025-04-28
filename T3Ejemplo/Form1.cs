using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using T3Ejemplo.Formularios;

namespace T3Ejemplo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Conexion.Conexion conexion = new Conexion.Conexion();
            //conexion.establecerConexion();
            //conexion.cerrarConexion();
        }

        private void administrarCursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Buscar si ya existe una instancia abierta del formulario
            FormCursos formAbierto = this.MdiChildren.OfType<FormCursos>().FirstOrDefault();

            if (formAbierto != null)
            {
                formAbierto.WindowState = FormWindowState.Normal;
                formAbierto.BringToFront();
                formAbierto.Focus();
            }
            else
            {
                FormCursos formCursos = new FormCursos();
                formCursos.MdiParent = this;
                formCursos.Show();
            }
        }

        private void administrarPersonasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPersonas formAbierto = this.MdiChildren.OfType<FormPersonas>().FirstOrDefault();

            if (formAbierto != null)
            {
                formAbierto.WindowState = FormWindowState.Normal;
                formAbierto.BringToFront();
                formAbierto.Focus();
            }
            else
            {
                FormPersonas formPersonas = new FormPersonas();
                formPersonas.MdiParent = this;
                formPersonas.Show();
            }
        }
    }
}
