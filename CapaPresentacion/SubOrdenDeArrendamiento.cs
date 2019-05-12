using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class SubOrdenDeArrendamiento : Form
    {

        private Principal principal;
        private N_OrdenDeArrendamiento n_OrdenDeArrendamiento = new N_OrdenDeArrendamiento();
        private N_Puesto n_Puesto = new N_Puesto();
        private N_Cliente n_Cliente = new N_Cliente();
        private N_Empleado n_Empleado = new N_Empleado();
        private E_OrdenDeArrendamiento e_OrdenDeArrendamiento = new E_OrdenDeArrendamiento();
        private int op = 0;

        public SubOrdenDeArrendamiento(Principal p)
        {
            InitializeComponent();
            principal = p;
        }

        private void radioButtonPorParametro_CheckedChanged(object sender, EventArgs e)
        {
            panelPorParametro.Visible = true;
            panelPorFecha.Visible = false;
        }

        private void radioButtonPorFecha_CheckedChanged(object sender, EventArgs e)
        {
            panelPorFecha.Visible = true;
            panelPorParametro.Visible = false;
        }

        private void SubOrdenDeArrendamiento_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                principal.Show();
                this.Dispose();
            }
            else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void SubOrdenDeArrendamiento_Load(object sender, EventArgs e)
        {
            buttonActualizarCliente_Click(null, null);
            dataGridViewOrdenesInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dateTimePickerFechaMin.MaxDate = DateTime.Today;

        }

        private void ListarOrdenesInfo()
        {
            DataTable dataTable = n_OrdenDeArrendamiento.ListarOrdenesInfo();
            dataGridViewOrdenesInfo.DataSource = dataTable;
        }
        
        private void buttonActualizarCliente_Click(object sender, EventArgs e)
        {
            ListarOrdenesInfo();
            AuxiliarCliente();
            AuxiliarEmpleado();
            AuxiliarPuesto();
            textBoxPuerto.Text = "" + n_Puesto.PuertoActual();
            LimpiarCamposInsercion();
        }

        private void AuxiliarCliente()
        {
            DataTable dataTable = n_Cliente.ListarClientes();
            comboBoxIDCliente.Items.Clear();
            foreach (DataRow dt in dataTable.Rows)
            {
                comboBoxIDCliente.Items.Add(dt["ID_CLIENTE"]);
            }
        }

        private void AuxiliarEmpleado()
        {
            DataTable dataTable = n_Empleado.ListarEmpleadosInfo();
            comboBoxIDEmpleado.Items.Clear();
            foreach (DataRow dt in dataTable.Rows)
            {
                comboBoxIDEmpleado.Items.Add(dt["ID_EMPLEADOI"]);
            }
        }

        private void AuxiliarPuesto()
        {
            DataTable dataTable = n_Puesto.ListarPuestosDisponibles();
            comboBoxIDPuesto.Items.Clear();
            foreach (DataRow dt in dataTable.Rows)
            {
                comboBoxIDPuesto.Items.Add(dt["ID_PUESTOP"]);
            }
        }

        private void textBoxBuscarNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void comboBoxBuscarOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarOrden.SelectedItem.ToString();
            if (criterioDeBusqueda == "Fecha de Órden")
            {
                panelNumero.Visible = false;
                panelFecha.Visible = true;
            }
            else
            {
                panelFecha.Visible = false;
                panelNumero.Visible = true;
            }
        }

        private void buttonBuscarOrdenFecha_Click(object sender, EventArgs e)
        {
            DataTable dataTableInfo = n_OrdenDeArrendamiento.BuscarRangoOrdenInfo(dateTimePickerDesde.Value, dateTimePickerHasta.Value);
            dataGridViewOrdenesInfo.DataSource = dataTableInfo;
        }

        private void buttonBuscarOrdenParamtero_Click(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarOrden.SelectedItem.ToString();
            DataTable dataTableInfo = new DataTable();
            if (criterioDeBusqueda == "Fecha de Órden")
            {
                dataTableInfo = n_OrdenDeArrendamiento.BuscarOrdenInfo(dateTimePickerBuscarFecha.Value);
            }
            else if (criterioDeBusqueda == "Cliente" || criterioDeBusqueda == "Empleado")
            {
                dataTableInfo = n_OrdenDeArrendamiento.BuscarOrdenInfo(criterioDeBusqueda, textBoxBuscarNumero.Text);
            }
            else
            {
                dataTableInfo = n_OrdenDeArrendamiento.BuscarOrdenInfo(criterioDeBusqueda, Convert.ToDouble(textBoxBuscarNumero.Text));
            }
            dataGridViewOrdenesInfo.DataSource = dataTableInfo;

        }

        private void buttonEditarCliente_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrdenesInfo.SelectedRows.Count > 0)
            {
                numericUpDownNumOrden.Value = Convert.ToInt16(dataGridViewOrdenesInfo.SelectedRows[0].Cells[0].Value);
                numericUpDownNumOrden.ReadOnly = true;
                comboBoxIDCliente.SelectedIndex = comboBoxIDCliente.FindStringExact(dataGridViewOrdenesInfo.SelectedRows[0].Cells[1].Value + string.Empty);
                textBoxPuerto.Text = dataGridViewOrdenesInfo.SelectedRows[0].Cells[2].Value + string.Empty;
                comboBoxIDPuesto.SelectedIndex = comboBoxIDPuesto.FindStringExact(dataGridViewOrdenesInfo.SelectedRows[0].Cells[3].Value + string.Empty);
                comboBoxIDPuesto.Enabled = false;
                comboBoxIDEmpleado.SelectedIndex = comboBoxIDEmpleado.FindStringExact(dataGridViewOrdenesInfo.SelectedRows[0].Cells[4].Value + string.Empty);
                dateTimePickerFechaMin.Value = Convert.ToDateTime(dataGridViewOrdenesInfo.SelectedRows[0].Cells[5].Value);
                dateTimePickerFechaMax.Value = Convert.ToDateTime(dataGridViewOrdenesInfo.SelectedRows[0].Cells[6].Value);
                String searchValue1 = numericUpDownNumOrden.Value + string.Empty;
                String searchValue2 = textBoxPuerto.Text;
                String searchValue3 = comboBoxIDPuesto.SelectedItem.ToString();
                op = 1;
            }
            else
            {

            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            e_OrdenDeArrendamiento.OrdenID = (int)numericUpDownNumOrden.Value;
            e_OrdenDeArrendamiento.ClienteID = comboBoxIDCliente.SelectedItem.ToString();
            e_OrdenDeArrendamiento.PuertoID = Convert.ToInt16(textBoxPuerto.Text);
            e_OrdenDeArrendamiento.PuestoID = Convert.ToInt16(comboBoxIDPuesto.SelectedItem.ToString());
            e_OrdenDeArrendamiento.EmpleadoID = comboBoxIDEmpleado.SelectedItem.ToString();
            e_OrdenDeArrendamiento.FechaInicio = dateTimePickerFechaMin.Value;
            e_OrdenDeArrendamiento.FechaFin = dateTimePickerFechaMax.Value;
            bool isSuccess = false;
            string operacion = "";
            if (op == 0)
            {
                isSuccess = n_OrdenDeArrendamiento.AgregarOrden(e_OrdenDeArrendamiento);
                operacion = "Inserción";
            }
            else
            {
                isSuccess = n_OrdenDeArrendamiento.EditarOrden(e_OrdenDeArrendamiento);
                operacion = "Actualización";
            }
            if (isSuccess)
            {
                DialogResult = MessageBox.Show("Operación Realizada con éxito", operacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposInsercion();
                ListarOrdenesInfo();
            }
            else
            {
                DialogResult = MessageBox.Show("Hubo un problema con la operación", operacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposInsercion()
        {
            numericUpDownNumOrden.Value = 1;
            numericUpDownNumOrden.ReadOnly = false;
            comboBoxIDCliente.SelectedIndex = 0;
            textBoxPuerto.Text = "" + n_Puesto.PuertoActual();
            comboBoxIDPuesto.SelectedIndex = 0;
            comboBoxIDPuesto.Enabled = true;
            comboBoxIDEmpleado.SelectedIndex = 0;
            dateTimePickerFechaMin.Value = DateTime.Today;
            dateTimePickerFechaMax.Value = DateTime.Today;
            numericUpDownMonto.Value = 1;
            op = 0;
        }

        private void buttonEliminarCliente_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea eliminar esta órden?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                bool isSuccess = n_OrdenDeArrendamiento.EliminarOrden((int)dataGridViewOrdenesInfo.SelectedRows[0].Cells[0].Value, (int)dataGridViewOrdenesInfo.SelectedRows[0].Cells[2].Value, (int)dataGridViewOrdenesInfo.SelectedRows[0].Cells[3].Value);

                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación realizada con éxito", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarOrdenesInfo();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dateTimePickerFechaMin_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFechaMax.MinDate = dateTimePickerFechaMin.Value;
        }

        private void dateTimePickerDesde_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerHasta.MinDate = dateTimePickerDesde.Value;
        }
    }
}