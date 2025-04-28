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
    public partial class FormCursosAgregar : Form
    {
        public FormCursosAgregar()
        {
            InitializeComponent();
            cargarDatosComboBox(); // carga los instructores, categorias, dificultades y estados de curso
        }

        private void cargarDatosComboBox()
        {
            Controladores.ControladorPersona controladorInstructores = new Controladores.ControladorPersona();
            Controladores.ControladorCategoria controladorCategoria = new Controladores.ControladorCategoria();
            Controladores.ControladorDificultad controladorDificultad = new Controladores.ControladorDificultad();
            Controladores.ControladorEstadosCurso controladorEstadosCurso = new Controladores.ControladorEstadosCurso();

            // Llamar a los métodos de consulta para llenar los ComboBox
            controladorInstructores.consultaInstructores(cbInstructores);
            controladorCategoria.consultaCategorias(cbCategoria);
            controladorDificultad.consultaDificultad(cbDificultad);
            controladorEstadosCurso.consultaEstadosCurso(cbEstadosCurso);
        }

        public void datosFormulario(Modelos.ModeloCursos objetoCurso)
        {
            objetoCurso.Titulo = tbTitulo.Text;
            objetoCurso.Precio = (decimal)numPrecio.Value;
            objetoCurso.Duracion_horas = (int)numDuracion.Value;
            objetoCurso.Descripcion = tbDescripcion.Text;
            objetoCurso.Instructor = ((DataRowView)cbInstructores.SelectedItem)["Id"].ToString();
            objetoCurso.Categoria = ((DataRowView)cbCategoria.SelectedItem)["Id"].ToString();
            objetoCurso.Dificultad = ((DataRowView)cbDificultad.SelectedItem)["Id"].ToString();
            objetoCurso.Estado = ((DataRowView)cbEstadosCurso.SelectedItem)["Id"].ToString();
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            if (string.IsNullOrWhiteSpace(tbTitulo.Text))
            {
                MessageBox.Show("El título es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (cbInstructores.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un instructor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (numPrecio.Value < 0)
            {
                MessageBox.Show("El precio debe ser mayor o igual a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (numDuracion.Value <= 0)
            {
                MessageBox.Show("La duración debe ser mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(tbDescripcion.Text))
            {
                MessageBox.Show("La descripción es requerida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (cbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (cbDificultad.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una dificultad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            if (cbEstadosCurso.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valido = false;
            }

            return valido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Modelos.ModeloCursos datosCursos = new Modelos.ModeloCursos();
                datosFormulario(datosCursos); // Llamar a la función para llenar el objeto con los datos del formulario


                Controladores.ControladorCurso controladorCurso = new Controladores.ControladorCurso();
                controladorCurso.agregarCurso(datosCursos);

                FormCursos formCursos = this.MdiParent.MdiChildren.OfType<FormCursos>().FirstOrDefault();
                formCursos.cargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el curso: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
