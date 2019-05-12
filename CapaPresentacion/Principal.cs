using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Principal : Form
    {
        private Cliente c;
        private SubEmpleado se;
        private SubOrdenDeArrendamiento so;
        private Puerto p;

        public Principal()
        {
            InitializeComponent();
        }

        private void pictureBoxClientes_Click(object sender, EventArgs e)
        {
            c = new Cliente(this);
            c.Show();
            this.Hide();
        }

        private void pictureBoxEmpleado_Click(object sender, EventArgs e)
        {
            se = new SubEmpleado(this);
            se.Show();
            this.Hide();
        }

        private void labelClientes_Click(object sender, EventArgs e)
        {
            c = new Cliente(this);
            c.Show();
            this.Hide();
        }

        private void labelEmpleado_Click(object sender, EventArgs e)
        {
            se = new SubEmpleado(this);
            se.Show();
            this.Hide();
        }

        private void labelOrdenDeArrendamiento_Click(object sender, EventArgs e)
        {
            so = new SubOrdenDeArrendamiento(this);
            so.Show();
            this.Hide();
        }

        private void pictureBoxOrdenDeArrendamiento_Click(object sender, EventArgs e)
        {
            so = new SubOrdenDeArrendamiento(this);
            so.Show();
            this.Hide();
        }

        private void labelPuerto_Click(object sender, EventArgs e)
        {
            p = new Puerto(this);
            p.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            p = new Puerto(this);
            p.Show();
            this.Hide();
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Realmente desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
