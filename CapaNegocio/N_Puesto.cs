using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class N_Puesto
    {
        private D_Puesto DataPuesto = new D_Puesto();

        public DataTable ListarPuestos()
        {
            return DataPuesto.ListarPuestos();
        }

        public int PuertoActual()
        {
            return DataPuesto.PuertoActual();
        }

        public DataTable ListarPuestosDisponibles()
        {
            return DataPuesto.ListarPuestosDisponibles();
        }

        public DataTable buscarPuesto(string criterioDeBusqueda, string texto)
        {
            if (texto == "Libre")
            {
                return DataPuesto.SelectEstadoPuesto("DIRECCION_P", 0);
            }
            else
            {
                return DataPuesto.SelectEstadoPuesto("DIRECCION_P", 1);
            }
        }

        public DataTable buscarPuesto(string criterioDeBusqueda, double valor)
        {
            if (criterioDeBusqueda == "ID")
            {
                return DataPuesto.SelectNumeroPuesto("ID_PUESTOP", valor);
            }
            else
            {
                return DataPuesto.SelectNumeroPuesto(criterioDeBusqueda, valor);
            }
        }

        public DataTable buscarPuesto(double minAncho, double maxAncho, double minLargo, double maxLargo)
        {
            return DataPuesto.SelectRangoPuesto(minAncho, maxAncho, minLargo, maxLargo);
        }

        public bool EditarEstado(int idPuerto, int idPuesto)
        {
            return DataPuesto.UpdateEstadoPuesto(idPuerto, idPuesto);
        }

    }
}
