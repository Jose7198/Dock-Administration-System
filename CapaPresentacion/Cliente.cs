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
    public partial class Cliente : Form
    {

        private N_Cliente n_Cliente = new N_Cliente();
        private N_Puerto n_Puerto = new N_Puerto();
        private E_Cliente e_Cliente = new E_Cliente();
        private N_Vehiculo n_Vehiculo = new N_Vehiculo();
        private E_Vehiculo e_Vehiculo = new E_Vehiculo();
        private Principal principal;
        private int operacion = 0;
        private int opVehiculo = 0;

        public Cliente(Principal p)
        {
            InitializeComponent();
            principal = p;
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            ListarClientes();
            ListarVehiculos();
            textBoxIDPuerto.Text = "" + n_Cliente.PuertoActual();
            textBoxPuerto.Text = "" + n_Cliente.PuertoActual();
            dateTimePickerFechaMin.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePickerFechaMax.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePickerFechaNac.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePickerFechaNac.Value = DateTime.Today.AddYears(-18);
            comboBoxBuscarCliente.SelectedIndex = 0;
            comboBoxBusqueda.SelectedIndex = 0;
            AuxiliarCliente();
            comboBoxCliente.SelectedIndex = 0;
            dataGridViewClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ListarClientes()
        {
            DataTable dataTable = n_Cliente.ListarClientes();
            dataGridViewClientes.DataSource = dataTable;
        }
        
        private void Cliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                principal.Show();
                this.Dispose();
            }else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void buttonBuscarCliente_Click(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarCliente.SelectedItem.ToString();
            DataTable dataTable = new DataTable();
            if (criterioDeBusqueda == "Rango de fechas")
            {
                dataTable = n_Cliente.BuscarRangoCliente(dateTimePickerFechaMin.Value, dateTimePickerFechaMax.Value);
            }
            else if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Teléfono")
            {
                dataTable = n_Cliente.BuscarTextoCliente(criterioDeBusqueda, textBoxNumeros.Text);
            }
            else
            {
                dataTable = n_Cliente.BuscarTextoCliente(criterioDeBusqueda, textBoxBuscarCliente.Text);
            }
            dataGridViewClientes.DataSource = dataTable;
        }

        private void comboBoxBuscarCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarCliente.SelectedItem.ToString();
            if (criterioDeBusqueda == "Rango de fechas")
            {
                panelNumero.Visible = false;
                panelTexto.Visible = false;
                panelRango.Visible = true;
            }
            else if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Teléfono")
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
                if (criterioDeBusqueda == "Dirección")
                {
                    textBoxBuscarCliente.MaxLength = 64;
                }
                else
                {
                    textBoxBuscarCliente.MaxLength = 32;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 45 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dateTimePickerFechaMin_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerFechaMax.MinDate = dateTimePickerFechaMin.Value;
        }

        private void buttonActualizarCliente_Click(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void buttonEditarCliente_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridViewClientes.SelectedRows[0].Cells[0].Value + string.Empty;
            textBoxID.ReadOnly = true;
            textBoxNombre.Text = dataGridViewClientes.SelectedRows[0].Cells[2].Value + string.Empty;
            textBoxApellido.Text = dataGridViewClientes.SelectedRows[0].Cells[3].Value + string.Empty;
            textBoxDireccion.Text = dataGridViewClientes.SelectedRows[0].Cells[4].Value + string.Empty;
            textBoxTelefono.Text = dataGridViewClientes.SelectedRows[0].Cells[5].Value + string.Empty;
            textBoxCorreo.Text = dataGridViewClientes.SelectedRows[0].Cells[6].Value + string.Empty;
            dateTimePickerFechaNac.Value = Convert.ToDateTime(dataGridViewClientes.SelectedRows[0].Cells[7].Value);
            dateTimePickerFechaNac.Enabled = false;
            operacion = 1;
        }

        private void buttonInsertarPuerto_Click_1(object sender, EventArgs e)
        {
            e_Cliente.IDCliente = textBoxID.Text;
            e_Cliente.IDPuerto = textBoxIDPuerto.Text;
            e_Cliente.Nombres = textBoxNombre.Text;
            e_Cliente.Apellidos = textBoxApellido.Text;
            e_Cliente.Direccion = textBoxDireccion.Text;
            e_Cliente.Telefono = textBoxTelefono.Text;
            e_Cliente.CorreoElectronico = textBoxCorreo.Text;
            e_Cliente.FechaDeNacimiento = dateTimePickerFechaNac.Value;
            bool isSuccess = false;
            string op = "";
            if (ValidaCedula(e_Cliente.IDCliente))
            {
                if (operacion == 0)
                {
                    isSuccess = n_Cliente.AgregarCliente(e_Cliente);
                    op = "Inserción";
                }
                else
                {
                    isSuccess = n_Cliente.EditarCliente(e_Cliente);
                    op = "Actualización";
                }
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación Realizada con éxito", op, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCamposInsercion();
                    operacion = 0;
                    ListarClientes();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", op, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Número de Cédula Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LimpiarCamposInsercion()
        {
            textBoxID.Text = string.Empty;
            textBoxID.ReadOnly = false;
            textBoxNombre.Text = string.Empty;
            textBoxApellido.Text = string.Empty;
            textBoxDireccion.Text = string.Empty;
            textBoxTelefono.Text = string.Empty;
            textBoxCorreo.Text = string.Empty;
            dateTimePickerFechaNac.Value = DateTime.Today.AddYears(-18);
            dateTimePickerFechaNac.Enabled = true;
        }

        private void buttonEliminarCliente_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea eliminar este cliente? Se eliminará todo vehículo asociado", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                bool isSuccess = n_Cliente.EliminarCliente(Convert.ToInt16(dataGridViewClientes.SelectedRows[0].Cells["ID_PUERTO"].Value), dataGridViewClientes.SelectedRows[0].Cells[0].Value + string.Empty);
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación realizada con éxito", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarClientes();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*-----------------------------------------------------------------------------------------
         -------------------------------------VEHÍCULOS--------------------------------------------
         ----------------------------------------------------------------------------------------*/

        private void buttonActualizarVehiculo_Click(object sender, EventArgs e)
        {
            ListarVehiculos();
            AuxiliarCliente();
        }

        private void ListarVehiculos()
        {
            DataTable dataTable = n_Vehiculo.ListarVehiculos();
            dataGridViewVehiculo.DataSource = dataTable;
        }

        private void textBoxBusquedaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void AuxiliarCliente()
        {
            DataTable dataTable = n_Cliente.BuscarTextoCliente("ID_PUERTO", Convert.ToInt16(textBoxPuerto.Text));
            comboBoxCliente.Items.Clear();
            foreach (DataRow dr in dataTable.Rows)
            {
                comboBoxCliente.Items.Add(dr["ID_CLIENTE"]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBusqueda.SelectedItem.ToString();
            if(criterioDeBusqueda == "Cliente")
            {
                panelTextoVehiculo.Visible = false;
                panelClienteID.Visible = true;
            }
            else
            {
                panelClienteID.Visible = false;
                panelTextoVehiculo.Visible = true;
            }
        }

        private void buttonEditarVehiculo_Click(object sender, EventArgs e)
        {
            textBoxPlaca.Text = dataGridViewVehiculo.SelectedRows[0].Cells[0].Value + string.Empty;
            textBoxPlaca.ReadOnly = true;
            comboBoxCliente.Text = dataGridViewVehiculo.SelectedRows[0].Cells[1].Value + string.Empty;
            textBoxMarca.Text = dataGridViewVehiculo.SelectedRows[0].Cells[2].Value + string.Empty;
            textBoxModelo.Text = dataGridViewVehiculo.SelectedRows[0].Cells[3].Value + string.Empty;
            textBoxMatricula.Text = dataGridViewVehiculo.SelectedRows[0].Cells[4].Value + string.Empty;
            opVehiculo = 1;
        }

        private void LimpiarCamposVehiculo()
        {
            textBoxPlaca.Text = string.Empty;
            textBoxPlaca.ReadOnly = false;
            comboBoxCliente.SelectedIndex = 0;
            textBoxMarca.Text = string.Empty;
            textBoxModelo.Text = string.Empty;
            textBoxMatricula.Text = string.Empty;
        }

        private void buttonGuardarCliente_Click(object sender, EventArgs e)
        {
            e_Vehiculo.ClienteID = comboBoxCliente.SelectedItem.ToString();
            e_Vehiculo.Placa = textBoxPlaca.Text;
            e_Vehiculo.Marca = textBoxMarca.Text;
            e_Vehiculo.Modelo = textBoxModelo.Text;
            e_Vehiculo.Matricula = textBoxMatricula.Text;
            e_Vehiculo.PuertoID = Convert.ToInt16(textBoxPuerto.Text);
            bool isSuccess = false;
            string op = "";
            if (opVehiculo == 0)
            {
                isSuccess = n_Vehiculo.AgregarVehiculo(e_Vehiculo);
                op = "Inserción";
            }
            else
            {
                isSuccess = n_Vehiculo.EditarVehiculo(e_Vehiculo);
                op = "Actualización";
            }
            if (isSuccess)
            {
                DialogResult = MessageBox.Show("Operación Realizada con éxito", op, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposVehiculo();
                opVehiculo = 0;
                ListarVehiculos();
            }
            else
            {
                DialogResult = MessageBox.Show("Hubo un problema con la operación", op, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEliminarVehiculo_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea eliminar este vehículo?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                bool isSuccess = n_Vehiculo.EliminarVehiculo(Convert.ToInt16(dataGridViewVehiculo.SelectedRows[0].Cells["ID_PUERTO"].Value), dataGridViewVehiculo.SelectedRows[0].Cells["PLACA"].Value + string.Empty);
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación realizada con éxito", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarVehiculos();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonBuscarVehiculo_Click(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBusqueda.SelectedItem.ToString();
            DataTable dataTable = new DataTable();
            if (criterioDeBusqueda == "Cliente")
            {
                dataTable = n_Vehiculo.BuscarVehiculos(criterioDeBusqueda, textBoxBusquedaCliente.Text);
            }
            else
            {
                dataTable = n_Vehiculo.BuscarVehiculos(criterioDeBusqueda, textBoxTextoCliente.Text);
            }
            dataGridViewVehiculo.DataSource = dataTable;
        }

        private void textBoxIDPuerto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxPuerto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
