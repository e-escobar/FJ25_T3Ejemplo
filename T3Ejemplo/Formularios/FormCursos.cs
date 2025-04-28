using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T3Ejemplo.Formularios
{
    public partial class FormCursos : Form
    {
        public FormCursos()
        {
            InitializeComponent();
            cargarDatos(); // cargar todos los cursos existentes
        }

        public void cargarDatos()
        {
            Controladores.ControladorCurso controladorCurso = new Controladores.ControladorCurso();
            controladorCurso.consultarCursos(dgCursos);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Formularios.FormCursosAgregar formCursosAgregar = new Formularios.FormCursosAgregar();
            formCursosAgregar.MdiParent = this.MdiParent;
            formCursosAgregar.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //seleccionar el registro del datagridview para enviarlo al formulario de editar
            if (dgCursos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgCursos.SelectedRows[0];
                int idCurso = Convert.ToInt32(row.Cells["Id"].Value);

                Formularios.FormCursosEditar formCursosEditar = new Formularios.FormCursosEditar(idCurso);
                formCursosEditar.MdiParent = this.MdiParent;
                formCursosEditar.Show();
            }
            else
            {
                MessageBox.Show("Seleccione un curso para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgCursos.SelectedRows.Count > 0)
            {
                // Obtener el registro seleccionado
                DataGridViewRow row = dgCursos.SelectedRows[0];
                int idCurso = Convert.ToInt32(row.Cells["Id"].Value);
                string nombreCurso = row.Cells["Titulo"].Value.ToString();

                // Mostrar diálogo de confirmación
                DialogResult respuesta = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el curso: {nombreCurso}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    // Proceder con la eliminación si el usuario confirma
                    Controladores.ControladorCurso controladorCurso = new Controladores.ControladorCurso();
                    controladorCurso.eliminarCurso(idCurso);
                    cargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un curso para eliminar.", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
