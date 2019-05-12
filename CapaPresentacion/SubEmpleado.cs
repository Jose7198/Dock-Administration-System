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
    public partial class SubEmpleado : Form
    {

        N_Puerto n_Puerto = new N_Puerto();
        N_Empleado n_Empleado = new N_Empleado();
        E_Empleado e_Empleado = new E_Empleado();
        private Principal principal;
        private int op = 0;

        public SubEmpleado(Principal p)
        {
            InitializeComponent();
            principal = p;
        }

        private void SubEmpleado_Load(object sender, EventArgs e)
        {
            ListarEmpleadosInfo();
            AuxiliarPuerto();
            AuxiliarEmpleado();
            comboBoxBuscarPuerto.SelectedIndex = 0;
            dateTimePickerFechaNac.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePickerHasta.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePickerDesde.MaxDate = DateTime.Today.AddYears(-18);
            comboBoxPuerto.SelectedIndex = 0;
            comboBoxEmpleado.SelectedIndex = 0;
            dataGridViewEmpleadoInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void AuxiliarPuerto()
        {
            DataTable dataTable = n_Puerto.ListarPuertos();
            comboBoxPuerto.Items.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                comboBoxPuerto.Items.Add(dr["ID_PUERTO"]);
            }
        }

        private void AuxiliarEmpleado()
        {
            DataTable dataTable = n_Empleado.BuscarPorPuerto(Convert.ToInt16(comboBoxPuerto.Items[0].ToString()) /*SelectedItem.ToString())*/);
            comboBoxEmpleado.Items.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                comboBoxEmpleado.Items.Add(dr["ID_EMPLEADOC"]);
            }
        }

        private void AuxiliarEmpleado(int indice)
        {
            DataTable dataTable = n_Empleado.BuscarPorPuerto(Convert.ToInt16(comboBoxPuerto.Items[indice].ToString()));
            comboBoxEmpleado.Items.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                comboBoxEmpleado.Items.Add(dr["ID_EMPLEADOC"]);
            }
        }

        private void ListarEmpleadosInfo()
        {
            DataTable dataTable = n_Empleado.ListarEmpleadosInfo();
            dataGridViewEmpleadoInfo.DataSource = dataTable;
        }

        private void SubEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                principal.Show();
                this.Dispose();
            }else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void buttonActualizarPuerto_Click(object sender, EventArgs e)
        {
            ListarEmpleadosInfo();
            AuxiliarPuerto();
            LimpiarCamposInsercion();
        }

        private void comboBoxPuerto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int aux = senderComboBox.SelectedIndex;
            AuxiliarEmpleado(aux);
        }
        
        private void textBoxBuscarNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar !=8)
            {
                e.Handled = true;
            }
        }

        private void comboBoxBuscarPuerto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarPuerto.SelectedItem.ToString();
            if (criterioDeBusqueda == "Rango de fechas de nacimiento")
            {
                panelNumero.Visible = false;
                panelTexto.Visible = false;
                panelRango.Visible = true;
            }
            else if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Puerto" || criterioDeBusqueda == "Salario")
            {
                panelRango.Visible = false;
                panelTexto.Visible = false;
                panelNumero.Visible = true;
            }
            else
            {
                panelRango.Visible = false;
                panelNumero.Visible = false;
                panelTexto.Visible = true;
            }
        }

        private void dateTimePickerDesde_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerHasta.MinDate = dateTimePickerDesde.Value;
        }

        private void buttonBuscarPuerto_Click(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarPuerto.SelectedItem.ToString();
            DataTable dataTableInfo = new DataTable();
            if (criterioDeBusqueda == "Rango de fechas de nacimiento")
            {
                dataTableInfo = n_Empleado.BuscarRangoEmpleadoInfo(dateTimePickerDesde.Value, dateTimePickerHasta.Value);
            }
            else if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Puerto")
            {
                dataTableInfo = n_Empleado.BuscarEmpleadoInfo(criterioDeBusqueda, Convert.ToInt16(textBoxBuscarNumero.Text));
            }
            else if (criterioDeBusqueda == "Nombre" || criterioDeBusqueda == "Apellido")
            {
                dataTableInfo = n_Empleado.BuscarEmpleadoInfo(criterioDeBusqueda, textBoxBuscarTexto.Text);
            }
            dataGridViewEmpleadoInfo.DataSource = dataTableInfo;
        }

        private void buttonEditarPuerto_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridViewEmpleadoInfo.SelectedRows[0].Cells[0].Value + string.Empty;
            textBoxID.ReadOnly = true;
            comboBoxPuerto.SelectedIndex = comboBoxPuerto.FindStringExact(dataGridViewEmpleadoInfo.SelectedRows[0].Cells[1].Value + string.Empty);
            comboBoxPuerto.Enabled = false;
            textBoxNombre.Text = dataGridViewEmpleadoInfo.SelectedRows[0].Cells[2].Value + string.Empty;
            textBoxApellido.Text = dataGridViewEmpleadoInfo.SelectedRows[0].Cells[3].Value + string.Empty;
            dateTimePickerFechaNac.Value = Convert.ToDateTime(dataGridViewEmpleadoInfo.SelectedRows[0].Cells[4].Value);
            dateTimePickerFechaNac.Enabled = false;
            op = 1;
        }

        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void buttonEliminarPuerto_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea eliminar este empleado?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                bool isSuccess = n_Empleado.EliminarEmpleado(dataGridViewEmpleadoInfo.SelectedRows[0].Cells[0].Value + string.Empty, Convert.ToInt16(dataGridViewEmpleadoInfo.SelectedRows[0].Cells["ID_PUERTO"].Value));
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación realizada con éxito", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarEmpleadosInfo();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool ValidaCedula(string cedula)
        {
            int numv = 10;
            int div = 11;
            int[] coeficientes;
            if (int.Parse(cedula[2].ToString()) < 6) { coeficientes = new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2 }; div = 10; }
            else
            {
                if (int.Parse(cedula[2].ToString()) == 6)
                {
                    coeficientes = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
                    numv = 9;
                }
                else coeficientes = new int[] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            }
            int total = 0;
            int numprovincia = 24;
            int calculo = 0;
            cedula = cedula.Replace("-", "");
            char[] valores = cedula.ToCharArray(0, 9);

            if (((Convert.ToInt16(valores[2].ToString()) <= 6) || (Convert.ToInt16(valores[2].ToString()) == 9)) && (Convert.ToInt16(cedula.Substring(0, 2)) <= numprovincia))
            {
                for (int i = 0; i < numv - 1; i++)
                {
                    calculo = (Convert.ToInt16(valores[i].ToString())) * coeficientes[i];
                    if (div == 10) total += calculo > 9 ? calculo - 9 : calculo;
                    else total += calculo;
                }
                return (div - (total % div)) >= 10 ? 0 == Convert.ToInt16(cedula[numv - 1].ToString()) : (div - (total % div)) == Convert.ToInt16(cedula[numv - 1].ToString());
            }
            else return false;
        }

        private void buttonInsertarPuerto_Click(object sender, EventArgs e)
        {
            e_Empleado.EmpleadoID = textBoxID.Text;
            e_Empleado.PuertoID = Convert.ToInt16(comboBoxPuerto.SelectedItem.ToString());
            e_Empleado.Nombres = textBoxNombre.Text;
            e_Empleado.Apellidos = textBoxApellido.Text;
            e_Empleado.FechaDeNacimiento = dateTimePickerFechaNac.Value;
            try
            {
                e_Empleado.Superior = comboBoxEmpleado.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                e_Empleado.Superior = "";
            }
            e_Empleado.Salario = (double) numericUpDownSalario.Value;
            e_Empleado.Cargo = textBoxCargo.Text;
            bool isSuccess = false;
            string operacion = "";
            if (ValidaCedula(e_Empleado.EmpleadoID))
            {
                if (op == 0)
                {
                    isSuccess = n_Empleado.AgregarEmpleado(e_Empleado);
                    operacion = "Inserción";
                }
                else
                {
                    isSuccess = n_Empleado.EditarEmpleado(e_Empleado);
                    operacion = "Actualización";
                }
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación Realizada con éxito", operacion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCamposInsercion();
                    ListarEmpleadosInfo();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", operacion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Número de Cédula Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposInsercion()
        {
            textBoxID.Text = string.Empty;
            textBoxID.ReadOnly = false;
            comboBoxPuerto.SelectedIndex = 0;
            comboBoxPuerto.Enabled = true;
            textBoxNombre.Text = string.Empty;
            textBoxApellido.Text = string.Empty;
            dateTimePickerFechaNac.Value = DateTime.Today.AddYears(-18);
            dateTimePickerFechaNac.Enabled = true;
            numericUpDownSalario.Value = 0;
            textBoxCargo.Text = string.Empty;
            op = 0;
        }
    }
}
