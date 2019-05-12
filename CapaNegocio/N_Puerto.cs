using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Data;



namespace CapaNegocio
{
    public class N_Puerto
    {
        private D_Puerto Puerto = new D_Puerto();

        public DataTable ListarPuertos()
        {
            return Puerto.ListarPuertos();
        }

        public bool EliminarPuerto(int puertoID)
        {
            return Puerto.DeletePuerto(puertoID);
        }

        public bool AgregarPuerto(E_Puerto e_Puerto)
        {
            return Puerto.InsertPuerto(e_Puerto.Id, e_Puerto.Nombre, e_Puerto.Capacidad, e_Puerto.Ciudad, e_Puerto.Direccion);
        }

        public bool EditarPuerto(E_Puerto e_Puerto)
        {
            return Puerto.UpdatePuerto(e_Puerto.Id, e_Puerto.Nombre, e_Puerto.Capacidad, e_Puerto.Ciudad, e_Puerto.Direccion);
        }

        public DataTable buscarPuerto(string criterioDeBusqueda, string texto)
        {
            if(criterioDeBusqueda == "Dirección")
            {
                return Puerto.SelectTextPuerto("DIRECCION_P", texto);
            }
            else
            {
                return Puerto.SelectTextPuerto(criterioDeBusqueda, texto);
            }
        }

        public DataTable buscarPuerto(string criterioDeBusqueda, int valor)
        {
            if (criterioDeBusqueda == "ID")
            {
                return Puerto.SelectNumeroPuerto("ID_PUERTO", valor);
            }
            else
            {
                return Puerto.SelectNumeroPuerto(criterioDeBusqueda, valor);
            }
        }

        public DataTable buscarPuerto(int min, int max)
        {
            return Puerto.SelectRangoPuerto(min, max);
        }

    }
}
