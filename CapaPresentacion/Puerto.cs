using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class Puerto : Form
    {

        private N_Puerto n_Puerto = new N_Puerto();
        private N_Puesto n_Puesto = new N_Puesto();
        private Principal principal;
        private E_Puerto e_Puerto = new E_Puerto();
        private int operacion = 0;
        private int criterio = 0;

        public Puerto(Principal p)
        {
            InitializeComponent();
            principal = p;
        }

        private void Puerto_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'puertoInfo.PUERTO' Puede moverla o quitarla según sea necesario.
            ListarPuertos();
            ListarPuestos();
            comboBoxBuscarPuerto.SelectedIndex = 0;
            comboBoxBuscarPuesto.SelectedIndex = 0;
            dataGridViewPuertos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPuestos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void ListarPuertos()
        {
            DataTable dataTable = n_Puerto.ListarPuertos();
            dataGridViewPuertos.DataSource = dataTable;
        }

        void ListarPuestos()
        {
            DataTable dataTable = n_Puesto.ListarPuestos();
            dataGridViewPuestos.DataSource = dataTable;
        }

        private void Puerto_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea salir?", "Salir", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dialog==DialogResult.Yes)
            {
                principal.Show();
                this.Dispose();
            }
            else if (dialog==DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListarPuestos();
        }

        private void buttonActualizarPuerto_Click(object sender, EventArgs e)
        {
            ListarPuertos();
            LimpiarCamposBusqueda();
        }

        private void buttonEliminarPuerto_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea eliminar el puerto?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                bool isSuccess = n_Puerto.EliminarPuerto(Convert.ToInt16(dataGridViewPuertos.SelectedRows[0].Cells["ID_PUERTO"].Value));
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación realizada con éxito", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarPuertos();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEditarPuerto_Click(object sender, EventArgs e)
        {
            numericUpDownID.Value = Convert.ToInt16(dataGridViewPuertos.SelectedRows[0].Cells[0].Value);
            numericUpDownID.ReadOnly = true;
            numericUpDownID.Increment = 0;
            textBoxNombre.Text = dataGridViewPuertos.SelectedRows[0].Cells[1].Value + string.Empty;
            numericUpDownCapacidad.Value = Convert.ToInt16(dataGridViewPuertos.SelectedRows[0].Cells[2].Value);
            numericUpDownCapacidad.ReadOnly = true;
            numericUpDownCapacidad.Increment = 0;
            textBoxCiudad.Text = dataGridViewPuertos.SelectedRows[0].Cells[3].Value + string.Empty;
            textBoxDireccion.Text = dataGridViewPuertos.SelectedRows[0].Cells[4].Value + string.Empty;
            operacion = 1;
        }

        private void buttonInsertarPuerto_Click(object sender, EventArgs e)
        {
            e_Puerto.Id = Convert.ToInt16(numericUpDownID.Value);
            e_Puerto.Nombre = textBoxNombre.Text;
            e_Puerto.Capacidad = Convert.ToInt16(numericUpDownCapacidad.Value);
            e_Puerto.Ciudad = textBoxCiudad.Text;
            e_Puerto.Direccion = textBoxDireccion.Text;
            bool isSuccess = false;
            string op = "";
            if (operacion == 0)
            {
                isSuccess =n_Puerto.AgregarPuerto(e_Puerto);
                op = "Inserción";
            }
            else
            {
                isSuccess = n_Puerto.EditarPuerto(e_Puerto);
                op = "Actualización";
            }
            if (isSuccess)
            {
                DialogResult = MessageBox.Show("Operación Realizada con éxito", op, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposInsercion();
                operacion = 0;
                ListarPuertos();
            }
            else
            {
                DialogResult = MessageBox.Show("Hubo un problema con la operación", op, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        /*-----------------------------------------------------------------------------------------
         -------------------------Limpiar TextBoxs y NumericUpDows---------------------------------
         ----------------------------------------------------------------------------------------*/

        private void LimpiarCamposInsercion()
        {
            numericUpDownID.Value = 1;
            numericUpDownID.ReadOnly = false;
            numericUpDownID.Increment = 1;
            textBoxNombre.Text = string.Empty;
            numericUpDownCapacidad.Value = 1;
            numericUpDownCapacidad.ReadOnly = false;
            numericUpDownCapacidad.Increment = 1;
            textBoxCiudad.Text = string.Empty;
            textBoxDireccion.Text = string.Empty;
        }

        private void LimpiarCamposBusqueda()
        {
            textBoxBuscarTexto.Text = string.Empty;
            numericUpDownBuscarNumero.Value = 1;
            numericUpDownCapacidadMin.Value = 1;
            numericUpDownCapacidadMax.Value = 99999;
        }

        /*-----------------------------------------------------------------------------------------
         ---------------------------Validar enteros NumericUpDows----------------------------------
         ----------------------------------------------------------------------------------------*/

        private void numericUpDownBuscarNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownCapacidadMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownCapacidadMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (criterio == 1)
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }

        }

        private void numericUpDownAnchoMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownAnchoMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownLargoMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void numericUpDownLargoMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 46 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        

        private void comboBoxBuscarPuerto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarPuerto.SelectedItem.ToString();
            if (criterioDeBusqueda == "Nombre" || criterioDeBusqueda == "Ciudad" || criterioDeBusqueda == "Direccion")
            {
                panelIntervalo.Visible = false;
                panelNumero.Visible = false;
                panelTexto.Visible = true;
            }else if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Capacidad")
            {
                panelIntervalo.Visible = false;
                panelTexto.Visible = false;
                panelNumero.Visible = true;
            }else if(criterioDeBusqueda == "Rango de Capacidad")
            {
                panelTexto.Visible = false;
                panelNumero.Visible = false;
                panelIntervalo.Visible = true;
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarPuerto.SelectedItem.ToString();
            DataTable dataTable = new DataTable();
            if (criterioDeBusqueda == "Nombre" || criterioDeBusqueda == "Ciudad" || criterioDeBusqueda == "Dirección")
            {
                dataTable = n_Puerto.buscarPuerto(criterioDeBusqueda,textBoxBuscarTexto.Text);
            }
            else if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Capacidad")
            {
                dataTable = n_Puerto.buscarPuerto(criterioDeBusqueda, Convert.ToInt16(numericUpDownBuscarNumero.Value));
            }
            else if (criterioDeBusqueda == "Rango de Capacidad")
            {
                dataTable = n_Puerto.buscarPuerto(Convert.ToInt16(numericUpDownCapacidadMin.Value), Convert.ToInt16(numericUpDownCapacidadMax.Value));
            }
            dataGridViewPuertos.DataSource = dataTable;
        }

        private void comboBoxBuscarPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarPuesto.SelectedItem.ToString();
            if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Ancho" || criterioDeBusqueda == "Largo")
            {
                panelIntervaloPuesto.Visible = false;
                panelEstado.Visible = false;
                panelNumeroPuesto.Visible = true;
                if(criterioDeBusqueda == "ID")
                {
                    numericUpDownBuscarNumero.DecimalPlaces = 0;
                    criterio = 1;
                }
                else
                {
                    numericUpDownBuscarNumero.DecimalPlaces = 2;
                    criterio = 0;
                }
            }
            else if (criterioDeBusqueda == "Estado")
            {
                panelIntervaloPuesto.Visible = false;
                panelNumeroPuesto.Visible = false;
                panelEstado.Visible = true;
            }
            else if (criterioDeBusqueda == "Intervalo de tamaño")
            {
                panelNumeroPuesto.Visible = false;
                panelEstado.Visible = false;
                panelIntervaloPuesto.Visible = true;
            }
        }

        private void buttonBuscarPuesto_Click(object sender, EventArgs e)
        {
            string criterioDeBusqueda = comboBoxBuscarPuesto.SelectedItem.ToString();
            DataTable dataTable = new DataTable();
            if (criterioDeBusqueda == "ID" || criterioDeBusqueda == "Ancho" || criterioDeBusqueda == "Largo")
            {
                dataTable = n_Puesto.buscarPuesto(criterioDeBusqueda, Convert.ToDouble(numericUpDownNumero.Value));
            }
            else if (criterioDeBusqueda == "Estado")
            {
                dataTable = n_Puesto.buscarPuesto(criterioDeBusqueda, comboBoxEstado.SelectedItem.ToString());
            }
            else if (criterioDeBusqueda == "Intervalo de tamaño")
            {
                dataTable = n_Puesto.buscarPuesto(Convert.ToDouble(numericUpDownAnchoMin.Value), Convert.ToDouble(numericUpDownAnchoMax.Value), Convert.ToDouble(numericUpDownLargoMin.Value), Convert.ToDouble(numericUpDownLargoMax.Value));
            }
            dataGridViewPuestos.DataSource = dataTable;
        }

        private void buttonOcuparDesocupar_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Desocupar el puesto?", "Desocupar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                bool isSuccess = n_Puesto.EditarEstado(Convert.ToInt16(dataGridViewPuestos.SelectedRows[0].Cells[0].Value), Convert.ToInt16(dataGridViewPuestos.SelectedRows[0].Cells[1].Value));
                if (isSuccess)
                {
                    DialogResult = MessageBox.Show("Operación Realizada con éxito","Desocupación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarPuestos();
                }
                else
                {
                    DialogResult = MessageBox.Show("Hubo un problema con la operación", "Desocupación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
