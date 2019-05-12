using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
    public class N_Cliente
    {
        private D_Cliente d_Cliente = new D_Cliente();

        public DataTable ListarClientes()
        {
            return d_Cliente.SelectCliente();
        }
        
        public int PuertoActual()
        {
            return d_Cliente.PuertoActual();
        }

        public bool AgregarCliente(E_Cliente e_Cliente)
        {
            return d_Cliente.InsertCliente(e_Cliente.IDCliente,e_Cliente.IDPuerto,e_Cliente.Nombres,e_Cliente.Apellidos,e_Cliente.Direccion,e_Cliente.Telefono,e_Cliente.CorreoElectronico,e_Cliente.FechaDeNacimiento);
        }

        public bool EditarCliente(E_Cliente e_Cliente)
        {
            return d_Cliente.UpdateCliente(e_Cliente.IDCliente, e_Cliente.IDPuerto, e_Cliente.Nombres, e_Cliente.Apellidos, e_Cliente.Direccion, e_Cliente.Telefono, e_Cliente.CorreoElectronico, e_Cliente.FechaDeNacimiento);
        }

        public DataTable BuscarRangoCliente(DateTime fechaMin, DateTime fechaMax)
        {
            return d_Cliente.SelectRangoCliente(fechaMin, fechaMax);
        }

        public DataTable BuscarTextoCliente(string criterioDeBusqueda, string atributo)
        {
            string aEnviar = "";
            if(criterioDeBusqueda == "ID")
            {
                aEnviar = "ID_CLIENTE";
            } else if (criterioDeBusqueda == "Nombre")
            {
                aEnviar = "NOMBRES";
            }else if (criterioDeBusqueda == "Apellido")
            {
                aEnviar = "APELLIDOS";
            }else if(criterioDeBusqueda == "Dirección")
            {
                aEnviar = "DIRECCION";
            }else if(criterioDeBusqueda == "Teléfono")
            {
                aEnviar = "TELEFONO";
            }else if(criterioDeBusqueda == "Correo Electrónico")
            {
                aEnviar = "CORREO_ELEC";
            }
            return d_Cliente.SelectTextoCliente(aEnviar, atributo);
        }

        public DataTable BuscarTextoCliente(string criterioDeBusqueda, int atributo)
        {
            return d_Cliente.SelectTextoCliente(criterioDeBusqueda, atributo);
        }

        public bool EliminarCliente(int idPuerto, string idCliente)
        {
            return d_Cliente.DeleteCliente(idPuerto, idCliente);
        }

    }
}
