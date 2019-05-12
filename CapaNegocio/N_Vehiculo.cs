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
    public class N_Vehiculo
    {

        private D_Vehiculo d_Vehiculo = new D_Vehiculo();

        public DataTable ListarVehiculos()
        {
            return d_Vehiculo.SelectVehiculo();
        } 

        public bool AgregarVehiculo(E_Vehiculo e_Vehiculo)
        {
            return d_Vehiculo.InsertVehiculo(e_Vehiculo.Placa, e_Vehiculo.ClienteID, e_Vehiculo.Marca, e_Vehiculo.Modelo, e_Vehiculo.Matricula, e_Vehiculo.PuertoID);
        }

        public bool EditarVehiculo(E_Vehiculo e_Vehiculo)
        {
            return d_Vehiculo.UpdateVehiculo(e_Vehiculo.Placa, e_Vehiculo.ClienteID, e_Vehiculo.Marca, e_Vehiculo.Modelo, e_Vehiculo.Matricula, e_Vehiculo.PuertoID);
        } 

        public bool EliminarVehiculo(int puertoID, string placa)
        {
            return d_Vehiculo.DeleteVehiculo(puertoID, placa);
        }

        public DataTable BuscarVehiculos(string criterioDeBusqueda, string value)
        {
            if(criterioDeBusqueda == "Cliente")
            {
                criterioDeBusqueda = "ID_CLIENTE";
            }else if(criterioDeBusqueda == "Matrícula")
            {
                criterioDeBusqueda = "MATRICULA";
            }
            return d_Vehiculo.SelectVehiculo(criterioDeBusqueda, value);
        }

    }
}
