using Microsoft.Reporting.WinForms;
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
    public partial class FormReportes : Form
    {
        public FormReportes(string opcion)
        {
            InitializeComponent();
            cargarReporte(opcion);
        }

        public void cargarReporte(string opcion)
        {
            DataSet dsReporte = new DataSet();
            Controladores.ControladorReporte objetoCursos = new Controladores.ControladorReporte();
            ReportDataSource rds = null;

            //switch con opcion string de tipo "inscritos" o "personas"
            switch (opcion)
            {
                case "inscritos":
                    objetoCursos.reporteInscripcionesPorCurso(dsReporte);
                    rds = new ReportDataSource("DataSetInscritos", dsReporte.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "T3Ejemplo.Reportes.ReporteInscritosCursos.rdlc";
                    break;
                case "personas":
                    objetoCursos.reportePersonas(dsReporte);
                    rds = new ReportDataSource("DataSetPersonas", dsReporte.Tables[0]);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "T3Ejemplo.Reportes.ReportePersonas.rdlc";
                    break;
                default:
                    MessageBox.Show("Opción no válida");
                    break;
            }

            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }

        private void FormReporteCursos_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }
    }
}
