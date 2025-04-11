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
            Controladores.ControladorCurso controladorCurso = new Controladores.ControladorCurso();
            controladorCurso.consultarCursos(dgCursos);
        }
    }
}
