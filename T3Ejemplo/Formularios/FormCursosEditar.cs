using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using T3Ejemplo.Controladores;

namespace T3Ejemplo.Formularios
{
    public partial class FormCursosEditar : Form
    {
        public FormCursosEditar(int idCurso)
        {
            InitializeComponent();

            Controladores.ControladorPersona controladorInstructores = new Controladores.ControladorPersona();
            Controladores.ControladorCategoria controladorCategoria = new Controladores.ControladorCategoria();
            Controladores.ControladorDificultad controladorDificultad = new Controladores.ControladorDificultad();
            Controladores.ControladorEstadosCurso controladorEstadosCurso = new Controladores.ControladorEstadosCurso();

            // Llamar a los métodos de consulta para llenar los ComboBox
            controladorInstructores.consultaInstructores(cbInstructores);
            controladorCategoria.consultaCategorias(cbCategoria);
            controladorDificultad.consultaDificultad(cbDificultad);
            controladorEstadosCurso.consultaEstadosCurso(cbEstadosCurso);

            Controladores.ControladorCurso controladorCurso = new Controladores.ControladorCurso();
            Modelos.ModeloCursos objetoCurso = new Modelos.ModeloCursos();
            controladorCurso.cursosPorId(idCurso, objetoCurso);

            llenarCampos(objetoCurso);
        }

        public static int idCurso;

        public void llenarCampos(Modelos.ModeloCursos curso)
        {
            if (curso == null) return;
            
            idCurso = curso.Id;
            tbTitulo.Text = curso.Titulo;
            numPrecio.Value = curso.Precio;
            numDuracion.Value = curso.Duracion_horas;
            tbDescripcion.Text = curso.Descripcion;

            SeleccionarItemPorValor(cbInstructores, curso.Instructor);
            SeleccionarItemPorValor(cbCategoria, curso.Categoria);
            SeleccionarItemPorValor(cbDificultad, curso.Dificultad);
            SeleccionarItemPorValor(cbEstadosCurso, curso.Estado);
        }

        private void SeleccionarItemPorValor(ComboBox comboBox, string valor)
        {
            foreach (DataRowView item in comboBox.Items)
            {
                if (item["Nombre"].ToString() == valor)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }
            comboBox.SelectedIndex = -1; // Si no se encuentra
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Modelos.ModeloCursos objetoCurso = new Modelos.ModeloCursos();
                objetoCurso.Id = idCurso;
                objetoCurso.Titulo = tbTitulo.Text;
                objetoCurso.Precio = Convert.ToDecimal(numPrecio.Value);
                objetoCurso.Duracion_horas = Convert.ToInt32(numDuracion.Value);
                objetoCurso.Descripcion = tbDescripcion.Text;

                objetoCurso.Instructor = ((DataRowView)cbInstructores.SelectedItem)["Id"].ToString();
                objetoCurso.Categoria = ((DataRowView)cbCategoria.SelectedItem)["Id"].ToString();
                objetoCurso.Dificultad = ((DataRowView)cbDificultad.SelectedItem)["Id"].ToString();
                objetoCurso.Estado = ((DataRowView)cbEstadosCurso.SelectedItem)["Id"].ToString();

                // Actualizar curso en la base de datos
                Controladores.ControladorCurso controladorCurso = new Controladores.ControladorCurso();
                controladorCurso.actualizarCurso(objetoCurso);

                FormCursos formCursos = this.MdiParent.MdiChildren.OfType<FormCursos>().FirstOrDefault();
                formCursos.cargarDatos();

            }

        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(tbTitulo.Text))
            {
                MessageBox.Show("El título es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (numPrecio.Value < 0)
            {
                MessageBox.Show("El precio debe ser mayor o igual a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (numDuracion.Value <= 0)
            {
                MessageBox.Show("La duración debe ser mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbDescripcion.Text))
            {
                MessageBox.Show("La descripción es requerida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
