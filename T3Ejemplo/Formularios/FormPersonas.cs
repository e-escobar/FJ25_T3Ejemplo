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
    public partial class FormPersonas : Form
    {
        public FormPersonas()
        {
            InitializeComponent();
            actualizarPersonas();
            cargarRoles();

        }

        public void actualizarPersonas()
        {
            Controladores.ControladorPersona controladorPersona = new Controladores.ControladorPersona();
            controladorPersona.consultarPersonas(dgPersonas);
        }

        public void cargarRoles()
        {
            Controladores.ControladorRol controladorRol = new Controladores.ControladorRol();
            controladorRol.consultaRoles(cbRoles);

        }

        private void pbFoto_Click(object sender, EventArgs e)
        {
            //Controladores.ControladorPersona controladorPersona = new Controladores.ControladorPersona();
            //controladorPersona.consultarFotoPersona(1, pbFoto);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //agregar los datos al modelo personas para enviarlo al controlador public void registrarPersona(Modelos.ModeloPersonas persona) {
            Controladores.ControladorPersona controladorPersona = new Controladores.ControladorPersona();
            Modelos.ModeloPersonas objetoPersona = new Modelos.ModeloPersonas();
            objetoPersona.Nombre = txtNombre.Text;
            objetoPersona.Email = txtEmail.Text;
            objetoPersona.Telefono = txtTelefono.Text;
            objetoPersona.Rol = cbRoles.SelectedValue.ToString();
            //objetoPersona.Foto = pbFoto.Image;

            controladorPersona.registrarPersona(objetoPersona);
            
            actualizarPersonas();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }
    }
}
