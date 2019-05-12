using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CapaDatos
{
    public class D_Vehiculo
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["sitedb"].ConnectionString);

        public DataTable SelectVehiculo()
        {
            SqlCommand command = new SqlCommand("sp_listarVehiculos", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectVehiculo(string criterioDeBusqueda, string value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM VEHICULO WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@value) + '%'", DB);
            command.Parameters.AddWithValue("@value", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool InsertVehiculo(string placa, string clienteID, string marca, string modelo, string matricula, int puertoID)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_insertarVehiculo", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@placa", placa);
                command.Parameters.AddWithValue("@id_Cliente", clienteID);
                command.Parameters.AddWithValue("@marca", marca);
                command.Parameters.AddWithValue("@modelo", modelo);
                command.Parameters.AddWithValue("@matricula", matricula);
                command.Parameters.AddWithValue("@id_Puerto", puertoID);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }

            }
            catch
            {

            }
            finally
            {
                DB.Close();
            }
            return isSuccess;
        }

        public bool UpdateVehiculo(string placa, string clienteID, string marca, string modelo, string matricula, int puertoID)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_actualizarVehiculo", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@placa", placa);
                command.Parameters.AddWithValue("@id_Cliente", clienteID);
                command.Parameters.AddWithValue("@marca", marca);
                command.Parameters.AddWithValue("@modelo", modelo);
                command.Parameters.AddWithValue("@matricula", matricula);
                command.Parameters.AddWithValue("@id_Puerto", puertoID);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }

            }
            catch
            {

            }
            finally
            {
                DB.Close();
            }
            return isSuccess;
        }

        public bool DeleteVehiculo(int puertoID, string placa)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_eliminarVehiculo", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPuerto", puertoID);
                command.Parameters.AddWithValue("@placa", placa);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
            }
            catch
            {

            }
            finally
            {
                DB.Close();
            }
            return isSuccess;
        }

    }
}
