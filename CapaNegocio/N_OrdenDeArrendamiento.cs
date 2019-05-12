using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;
using CapaEntidad;

namespace CapaNegocio
{
    public class N_OrdenDeArrendamiento
    {

        private D_OrdenDeArrendamiento d_OrdenDeArrendamiento = new D_OrdenDeArrendamiento();

        public DataTable ListarOrdenesInfo()
        {
            return d_OrdenDeArrendamiento.SelectOrdenesInfo();
        }

        public DataTable ListarOrdenesCont()
        {
            return d_OrdenDeArrendamiento.SelectOrdenesCont();
        }

        public DataTable BuscarRangoOrdenCont(DateTime fechaMin, DateTime fechaMax)
        {
            return d_OrdenDeArrendamiento.SelectRangoOrdenCont(fechaMin, fechaMax);
        }

        public DataTable BuscarRangoOrdenInfo(DateTime fechaMin, DateTime fechaMax)
        {
            return d_OrdenDeArrendamiento.SelectRangoOrdenInfo(fechaMin, fechaMax);
        }

        public DataTable BuscarOrdenCont(DateTime fecha)
        {
            return d_OrdenDeArrendamiento.SelectOrdenesCont(fecha);
        }

        public DataTable BuscarOrdenCont(string criterio, double value)
        {
            if(criterio == "ID")
            {
                criterio = "NUMORDENC";
            }else if(criterio == "Puerto")
            {
                criterio = "ID_PUERTOC";
            }
            else
            {
                criterio = "ID_PUESTOC";
            }
            return d_OrdenDeArrendamiento.SelectOrdenesCont(criterio, value);
        }

        public DataTable BuscarOrdenInfo(DateTime fecha)
        {
            return d_OrdenDeArrendamiento.SelectOrdenesInfo(fecha);
        }

        public DataTable BuscarOrdenInfo(string criterio, double value)
        {
            if (criterio == "ID")
            {
                criterio = "NUMORDENI";
            }
            else if (criterio == "Puerto")
            {
                criterio = "ID_PUERTOI";
            }
            else
            {
                criterio = "ID_PUESTOI";
            }
            return d_OrdenDeArrendamiento.SelectOrdenesInfo(criterio, value);
        }

        public DataTable BuscarOrdenInfo(string criterio, string value)
        {
            if (criterio == "Cliente")
            {
                criterio = "ID_CLIENTE";
            }
            else
            {
                criterio = "ID_EMPLEADO";
            }
            return d_OrdenDeArrendamiento.SelectOrdenesInfo(criterio, value);
        }

        public bool AgregarOrden(E_OrdenDeArrendamiento e_OrdenDeArrendamiento)
        {
            return d_OrdenDeArrendamiento.InsertOrdenes(e_OrdenDeArrendamiento.OrdenID, e_OrdenDeArrendamiento.ClienteID, e_OrdenDeArrendamiento.PuertoID, e_OrdenDeArrendamiento.PuestoID, e_OrdenDeArrendamiento.EmpleadoID, e_OrdenDeArrendamiento.FechaInicio, e_OrdenDeArrendamiento.FechaFin, e_OrdenDeArrendamiento.Cuota);
        }

        public bool EditarOrden(E_OrdenDeArrendamiento e_OrdenDeArrendamiento)
        {
            return d_OrdenDeArrendamiento.UpdateOrdenes(e_OrdenDeArrendamiento.OrdenID, e_OrdenDeArrendamiento.ClienteID, e_OrdenDeArrendamiento.PuertoID, e_OrdenDeArrendamiento.PuestoID, e_OrdenDeArrendamiento.EmpleadoID, e_OrdenDeArrendamiento.FechaInicio, e_OrdenDeArrendamiento.FechaFin, e_OrdenDeArrendamiento.Cuota);
        }

        public bool EliminarOrden(int ordenID, int puertoID, int puestoID)
        {
            return d_OrdenDeArrendamiento.DeleteOrdenes(ordenID, puertoID, puestoID);
        }

    }
}
