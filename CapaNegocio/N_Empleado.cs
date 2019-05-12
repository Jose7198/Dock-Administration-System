using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class N_Empleado
    {

        private D_Empleado d_Empleado = new D_Empleado();

        public DataTable ListarEmpleadosInfo()
        {
            return d_Empleado.ListarEmpleadosInfo();
        }

        public DataTable ListarEmpleadosCont()
        {
            return d_Empleado.ListarEmpleadosCont();
        }

        public DataTable BuscarPorPuerto(int puerto)
        {
            return d_Empleado.BuscarPorPuerto(puerto);
        }

        public DataTable BuscarRangoEmpleadoInfo(DateTime fechaInicio, DateTime fechaFin)
        {
            return d_Empleado.SelectRangoEmpleadoInfo(fechaInicio, fechaFin);
        }

        public DataTable BuscarEmpleadoInfo(string criterioDeBusqueda, string valor)
        {
            if(criterioDeBusqueda != "Cargo")
            {
                criterioDeBusqueda += "s";
            }
            return d_Empleado.SelectEmpleadoInfo(criterioDeBusqueda, valor);
        }

        public DataTable BuscarEmpleadoInfo(string criterioDeBusqueda, int valor)
        {
            if (criterioDeBusqueda == "Puerto")
            {
                criterioDeBusqueda = "ID_PUERTO";
            }
            else if(criterioDeBusqueda == "ID")
            {
                criterioDeBusqueda += "_EMPLEADOI";
            }
            return d_Empleado.SelectEmpleadoInfo(criterioDeBusqueda, valor);
        }

        public DataTable BuscarEmpleadoCont(string criterioDeBusqueda, string valor)
        {
            if (criterioDeBusqueda != "Cargo")
            {
                criterioDeBusqueda += "s";
            }
            return d_Empleado.SelectEmpleadoCont(criterioDeBusqueda, valor);
        }

        public DataTable BuscarEmpleadoCont(string criterioDeBusqueda, double valor)
        {
            if (criterioDeBusqueda == "Puerto")
            {
                criterioDeBusqueda = "ID_PUERTO";
            }
            else if (criterioDeBusqueda == "ID")
            {
                criterioDeBusqueda += "_EMPLEADOC";
            }
            return d_Empleado.SelectEmpleadoCont(criterioDeBusqueda, valor);
        }

        public bool AgregarEmpleado(E_Empleado e_Empleado)
        {
            return d_Empleado.InsertEmpleado(e_Empleado.EmpleadoID, e_Empleado.PuertoID, e_Empleado.Nombres, e_Empleado.Apellidos, e_Empleado.FechaDeNacimiento, e_Empleado.Superior, e_Empleado.Salario, e_Empleado.Cargo);
        }

        public bool EditarEmpleado(E_Empleado e_Empleado)
        {
            return d_Empleado.UpdateEmpleado(e_Empleado.EmpleadoID, e_Empleado.PuertoID, e_Empleado.Nombres, e_Empleado.Apellidos, e_Empleado.FechaDeNacimiento, e_Empleado.Superior, e_Empleado.Salario, e_Empleado.Cargo);
        }

        public bool EliminarEmpleado(string empleadoID, int puertoID)
        {
            return d_Empleado.DeleteEmpleado(empleadoID, puertoID);
        }

    }
}
